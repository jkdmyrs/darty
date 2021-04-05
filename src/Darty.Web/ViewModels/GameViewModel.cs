namespace Darty.Web.ViewModels
{
    using Darty.Core.Resources.Responses;

    public class GameViewModel
    {
        public GameViewModel(GameModelResponse model)
        {
            Player1 = new PlayerViewModel
            {
                IsWinner = model.Player1.IsWinner,
                Name = model.Player1.Name,
                Score = model.Player1.Score
            };
            Player2 = new PlayerViewModel
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
    }
}
