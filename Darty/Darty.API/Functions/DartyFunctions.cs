namespace Darty.API.Functions
{
    using System;
    using System.Threading.Tasks;
    using Darty.API.Common;
    using Darty.Core.Exceptions;
    using Darty.Core.Models;
    using Darty.Core.Operations.Interfaces;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Microsoft.Extensions.Logging;

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
        public async Task<IActionResult> CreateGame([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "darty/game")]
            HttpRequest req)
        {
            string player1 = req.Query["player1"];
            string player2 = req.Query["player2"];
            return await this.RunAsync<string>(_createGame.Execute(player1, player2)).ConfigureAwait(false);
        }

        [FunctionName(Constants.GetGameById)]
        public async Task<IActionResult> GetGameById([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "darty/game")]
            HttpRequest req)
        {
            string gameId = req.Query["game"];
            return await this.RunAsync<CricketGameModel>(_getGame.Execute(gameId)).ConfigureAwait(false);
        }

        [FunctionName(Constants.DartThrow)]
        public async Task<IActionResult> DartThrow([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "darty/dart-throw")]
            HttpRequest req)
        {
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
            return await this.RunAsync<CricketGameModel>(_dartThrow.Execute(gameId, player, value, multiplier)).ConfigureAwait(false);
        }
    }
}

