namespace Darty.Core.Operations.Impl
{
    using Darty.Core.Models;
    using Darty.Core.Operations.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public class DartThrowOperation : IDartThrowOperation
    {
        public Task<CricketGameModel> Execute(string gameId, string player, int value, int multiplier)
        {
            throw new NotImplementedException();
        }
    }
}
