namespace Darty.Core.Operations.Impl
{
    using Darty.Core.Commands.Interfaces;
    using Darty.Core.Mappers;
    using Darty.Core.Models;
    using Darty.Core.Operations.Interfaces;
    using System;
    using System.Threading.Tasks;

    public class CreateGameOperation : ICreateGameOperation
    {
        private readonly IPersistGameCommand _persistGame;

        public CreateGameOperation(IPersistGameCommand persistGame)
        {
            _persistGame = persistGame ?? throw new ArgumentNullException(nameof(persistGame));
        }

        public async Task Execute(string player1, string player2, string gameId)
        {
            var game = new GameModel(player1, player2, gameId);
            await _persistGame.Execute(game.MapToDataResource()).ConfigureAwait(false);
        }
    }
}
