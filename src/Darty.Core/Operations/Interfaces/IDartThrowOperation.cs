namespace Darty.Core.Operations.Interfaces
{
    using System.Threading.Tasks;

    public interface IDartThrowOperation
    {
        Task Execute(string gameId, string player, int value, int multiplier);
    }
}
