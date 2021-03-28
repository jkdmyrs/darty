namespace Darty.Core.Operations.Impl
{
    using Darty.Core.Commands.Interfaces;
    using Darty.Core.Mappers;
    using Darty.Core.Models;
    using Darty.Core.Operations.Interfaces;
    using System;
    using System.Threading.Tasks;

    public class GetGameByIdOperation : IGetGameByIdOperation
    {
        private readonly IFetchGameCommand _fetchGame;

        public GetGameByIdOperation(IFetchGameCommand fetchGame)
        {
            _fetchGame = fetchGame ?? throw new ArgumentNullException(nameof(fetchGame));
        }

        public async Task<GameModel> Execute(string gameId)
        {
            var gameResource = await _fetchGame.Execute(gameId).ConfigureAwait(false);
            return gameResource.MapToModel();
        }
    }
}
