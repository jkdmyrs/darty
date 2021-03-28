namespace Darty.Core.Commands.Impl
{
    using Darty.Core.Commands.Interfaces;
    using Darty.Core.Resources.Data;
    using System;
    using System.Threading.Tasks;

    public class FetchGameCommand : IFetchGameCommand
    {
        public async Task<GameModelResource> Execute(string id)
        {
            throw new NotImplementedException();
        }
    }
}
