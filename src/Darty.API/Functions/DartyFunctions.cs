namespace Darty.API.Functions
{
    using Darty.API.Common;
    using Darty.Core.Exceptions;
    using Darty.Core.Mappers;
    using Darty.Core.Operations.Interfaces;
    using Darty.Core.Resources.Responses;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Threading.Tasks;
    using Microsoft.Azure.WebJobs.Extensions.SignalRService;
    using Darty.API.Constants;
    using Darty.Core;

    public class DartyFunctions : BaseDartyFunctionHandler
    {
        private readonly ICreateGameOperation _createGame;
        private readonly IDartThrowOperation _dartThrow;
        private readonly IGetGameByIdOperation _getGame;
        private readonly ICreateGameIdOperation _generateGameId;

        public DartyFunctions(ICreateGameOperation createGame,
                              IDartThrowOperation dartThrow,
                              IGetGameByIdOperation getGame,
                              ICreateGameIdOperation generateGameId,
                              ILogger<DartyFunctions> logger)
            : base (logger)
        {
            _createGame = createGame ?? throw new ArgumentNullException(nameof(createGame));
            _dartThrow = dartThrow ?? throw new ArgumentNullException(nameof(dartThrow));
            _getGame = getGame ?? throw new ArgumentNullException(nameof(getGame));
            _generateGameId = generateGameId ?? throw new ArgumentNullException(nameof(generateGameId));
        }

        [FunctionName(FunctionNames.CreateGame)]
        public async Task<IActionResult> CreateGame(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "game")] HttpRequest req,
            [SignalR(HubName = HubNames.GameHub)] IAsyncCollector<SignalRMessage> signalRMessages)
        {
            // get params
            string gameId = req.Query["game"];
            string player1 = req.Query["player1"];
            string player2 = req.Query["player2"];

            // create work
            async Task CreateGameAndSendSignalRMessage()
            {
                await _createGame.Execute(player1, player2, gameId).ConfigureAwait(false);
                await signalRMessages.AddAsync(new SignalRMessage
                {
                    UserId = gameId, 
                    Target = Targets.NewGame,
                    Arguments = Array.Empty<object>()
                }).ConfigureAwait(false);
            }

            // run
            return await RunAsync(CreateGameAndSendSignalRMessage()).ConfigureAwait(false);
        }

        [FunctionName(FunctionNames.GetGameById)]
        public async Task<IActionResult> GetGameById(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "game")] HttpRequest req)
        {
            // get params
            string gameId = req.Query["game"];
            
            // create work
            async Task<GameModelResponse> GetGameAndMapToResponse()
                => (await _getGame.Execute(gameId).ConfigureAwait(false)).MapToResponse();
            
            // run
            return await RunAsync<GameModelResponse>(GetGameAndMapToResponse()).ConfigureAwait(false);
        }

        [FunctionName(FunctionNames.DartThrow)]
        public async Task<IActionResult> DartThrow(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "throw")] HttpRequest req,
            [SignalR(HubName = HubNames.GameHub)] IAsyncCollector<SignalRMessage> signalRMessages)
        {
            // get params
            string gameId = req.Query["game"];
            string player = req.Query["player"];
            if (!int.TryParse(req.Query["val"], out int value))
            {
                throw new InvalidDartValueException(req.Query["val"]);
            }
            if (!int.TryParse(req.Query["mult"], out int multiplier))
            {
                throw new InvalidDartMultiplierException(req.Query["mult"]);
            }
            
            // create work
            async Task DartThrowAndSendSignalRMessage()
            {
                await _dartThrow.Execute(gameId, player, value, multiplier).ConfigureAwait(false);
                await signalRMessages.AddAsync(new SignalRMessage
                {
                    UserId = gameId,
                    Target = Targets.DartThrow,
                    Arguments = Array.Empty<object>()
                }).ConfigureAwait(false);
            }

            // run
            return await RunAsync(DartThrowAndSendSignalRMessage()).ConfigureAwait(false);
        }

        [FunctionName(FunctionNames.GenerateGameId)]
        public async Task<IActionResult> GenerateGameId([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "id")]
            HttpRequest req)
        {
            // run
            return await RunAsync<string>(_generateGameId.Execute()).ConfigureAwait(false);
        }

        [FunctionName(FunctionNames.NegotiateSignalR)]
        public SignalRConnectionInfo GetSignalRInfo(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "negotiate")] HttpRequest req, 
            [SignalRConnectionInfo(HubName = HubNames.GameHub, UserId = "{headers.x-ms-signalr-userid}")] SignalRConnectionInfo connectionInfo)
        {
            return connectionInfo;
        }
    }
}

