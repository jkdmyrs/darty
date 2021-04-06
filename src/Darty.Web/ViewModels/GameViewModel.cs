namespace Darty.Web.ViewModels
{
    using Darty.Core.Resources.Responses;
    using System.Linq;

    public class GameViewModel
    {
        public GameViewModel(GameModelResponse model)
        {
            Player1 = new PlayerViewModel(model.ThrowHistory.Where(x => x.Player == model.Player1.Name))
            {
                IsWinner = model.Player1.IsWinner,
                Name = model.Player1.Name,
                Score = model.Player1.Score
            };
            Player2 = new PlayerViewModel(model.ThrowHistory.Where(x => x.Player == model.Player2.Name))
            {
                IsWinner = model.Player2.IsWinner,
                Name = model.Player2.Name,
                Score = model.Player2.Score
            };
            HasWinner = model.HasWinner;
        }

        public PlayerViewModel Player1 { get; set; }
        public PlayerViewModel Player2 { get; set; }
        public bool HasWinner { get; set; }
        public string WinnerName => HasWinner ? string.Empty :
            Player1.IsWinner ? Player1.Name : Player2.Name;
    }
}
