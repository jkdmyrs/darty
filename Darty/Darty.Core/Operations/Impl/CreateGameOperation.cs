namespace Darty.Core.Operations.Impl
{
    using Darty.Core.Models;
    using Darty.Core.Operations.Interfaces;
    using System.Threading.Tasks;

    public class CreateGameOperation : ICreateGameOperation
    {
        public async Task<string> Execute(string player1, string player2)
        {
            var game = new CricketGameModel(player1, player2);
            // todo - persist 
            return game.Id.ToString();
        }
    }
}
