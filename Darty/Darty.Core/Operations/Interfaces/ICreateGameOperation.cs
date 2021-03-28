namespace Darty.Core.Operations.Interfaces
{
    using System.Threading.Tasks;

    public interface ICreateGameOperation
    {
        Task<string> Execute(string player1, string player2);
    }
}
