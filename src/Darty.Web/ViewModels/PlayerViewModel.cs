using Darty.Core.Resources.Responses;
using System.Collections.Generic;

namespace Darty.Web.ViewModels
{
    public class PlayerViewModel
    {
        private List<DartThrowResponse> _dartThrows;

        public string Name { get; set; }
        public bool IsWinner { get; set; }
        public int Score { get; set; }
    }
}
