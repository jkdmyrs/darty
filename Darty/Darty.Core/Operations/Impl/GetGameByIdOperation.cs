namespace Darty.Core.Operations.Impl
{
    using Darty.Core.Models;
    using Darty.Core.Operations.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public class GetGameByIdOperation : IGetGameByIdOperation
    {
        public Task<CricketGameModel> Execute(string gameId)
        {
            throw new NotImplementedException();
        }
    }
}
