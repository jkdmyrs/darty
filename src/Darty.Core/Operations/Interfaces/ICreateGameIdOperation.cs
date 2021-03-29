namespace Darty.Core.Operations.Interfaces
{
    using System.Threading.Tasks;

    public interface ICreateGameIdOperation
    {
        Task<string> Execute();
    }
}
