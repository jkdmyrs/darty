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

    public class DartyFunctions : BaseDartyFunctionHandler
    {
        private readonly ICreateGameOperation _createGame;
        private readonly IDartThrowOperation _dartThrow;
        private readonly IGetGameByIdOperation _getGame;

        public DartyFunctions(ICreateGameOperation createGame,
                              IDartThrowOperation dartThrow,
                              IGetGameByIdOperation getGame,
                              ILogger<DartyFunctions> logger)
            : base (logger)
        {
            _createGame = createGame ?? throw new ArgumentNullException(nameof(createGame));
            _dartThrow = dartThrow ?? throw new ArgumentNullException(nameof(dartThrow));
            _getGame = getGame ?? throw new ArgumentNullException(nameof(getGame));
        }

        [FunctionName(Constants.CreateGame)]
        public async Task<IActionResult> CreateGame([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "game")]
            HttpRequest req)
        {
            // get params
            string player1 = req.Query["player1"];
            string player2 = req.Query["player2"];

            // run
            return await this.RunAsync<string>(_createGame.Execute(player1, player2)).ConfigureAwait(false);
        }

        [FunctionName(Constants.GetGameById)]
        public async Task<IActionResult> GetGameById([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "game")]
            HttpRequest req)
        {
            // get params
            string gameId = req.Query["game"];
            
            // create work
            async Task<GameModelResponse> GetGameAndMapToResponse()
                => (await _getGame.Execute(gameId).ConfigureAwait(false)).MapToResponse();
            
            // run
            return await this.RunAsync<GameModelResponse>(GetGameAndMapToResponse()).ConfigureAwait(false);
        }

        [FunctionName(Constants.DartThrow)]
        public async Task<IActionResult> DartThrow([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "dart-throw")]
            HttpRequest req)
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
            async Task<GameModelResponse> DartThrowAndMapToResponse()
                => (await _dartThrow.Execute(gameId, player, value, multiplier).ConfigureAwait(false)).MapToResponse();

            // run
            return await this.RunAsync<GameModelResponse>(DartThrowAndMapToResponse()).ConfigureAwait(false);
        }
    }
}

