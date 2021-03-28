namespace Darty.Core.Commands.Impl
{
    using Darty.Core.Commands.Interfaces;
    using Darty.Core.Resources.Data;
    using System;
    using System.Threading.Tasks;

    public class PersistGameCommand : IPersistGameCommand
    {
        public Task Execute(GameModelResource game)
        {
            throw new NotImplementedException();
        }
    }
}
