namespace Darty.Core.Commands.Interfaces
{
    using Darty.Core.Resources.Data;
    using System.Threading.Tasks;

    public interface IFetchGameCommand
    {
        Task<GameModelResource> Execute(string id);
    }
}
