namespace Darty.Core.Operations.Interfaces
{
    using Darty.Core.Models;
    using System.Threading.Tasks;

    public interface IDartThrowOperation
    {
        Task<GameModel> Execute(string gameId, string player, int value, int multiplier);
    }
}
