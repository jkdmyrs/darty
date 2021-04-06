namespace Darty.Web.ViewModels
{
    using Darty.Core.Resources.Responses;
    using System.Collections.Generic;
    using System.Linq;

    public class PlayerViewModel
    {
        private List<DartThrowResponse> _dartThrows;

        public PlayerViewModel(IEnumerable<DartThrowResponse> dartThrows)
        {
            _dartThrows = dartThrows.ToList();
        }

        public string Name { get; set; }
        public bool IsWinner { get; set; }
        public int Score { get; set; }
        public (bool IsClosed, int HitCount) GetScoreDetailsForValue(int value)
        {
            int hitCount = _dartThrows.Where(x => x.Value == value).Sum(y => y.Multiplier);
            return (hitCount >= 3, hitCount);
        }
    }
}
