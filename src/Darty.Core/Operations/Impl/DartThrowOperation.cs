namespace Darty.Core.Operations.Impl
{
    using Darty.Core.Commands.Interfaces;
    using Darty.Core.Mappers;
    using Darty.Core.Operations.Interfaces;
    using System;
    using System.Threading.Tasks;

    public class DartThrowOperation : IDartThrowOperation
    {
        private readonly IGetGameByIdOperation _getGame;
        private readonly IPersistGameCommand _persistGame;

        public DartThrowOperation(IGetGameByIdOperation getGame, IPersistGameCommand persistGame)
        {
            _persistGame = persistGame ?? throw new ArgumentNullException(nameof(persistGame));
            _getGame = getGame ?? throw new ArgumentException(nameof(getGame));
        }

        public async Task Execute(string gameId, string player, int value, int multiplier)
        {
            var game = await _getGame.Execute(gameId).ConfigureAwait(false);
            game.DartThrow(player, value, multiplier);
            await _persistGame.Execute(game.MapToDataResource()).ConfigureAwait(false);
        }
    }
}
