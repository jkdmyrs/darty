namespace Darty.Core.Operations.Interfaces
{
    using Darty.Core.Models;
    using System.Threading.Tasks;

    public interface IGetGameByIdOperation
    {
        Task<GameModel> Execute(string gameId);
    }
}
