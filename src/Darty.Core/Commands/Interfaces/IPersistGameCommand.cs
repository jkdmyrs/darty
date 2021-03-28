namespace Darty.Core.Commands.Interfaces
{
    using Darty.Core.Resources.Data;
    using System.Threading.Tasks;

    public interface IPersistGameCommand
    {
        Task Execute(GameModelResource game);
    }
}
