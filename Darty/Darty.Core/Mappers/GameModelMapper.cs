namespace Darty.Core.Mappers
{
    using Darty.Core.Models;
    using Darty.Core.Resources.Data;
    using Darty.Core.Resources.Responses;
    using System.Linq;

    public static class GameModelMapper
    {
        public static GameModelResource MapToDataResource(this GameModel game)
        {
            return new GameModelResource
            {
                Id = game.Id.ToString(),
                Player1 = new PlayerResource
                {
                    IsWinner = game.Player1.IsWinner,
                    Name = game.Player1.Name,
                    Score = game.Player1.Score
                },
                Player2 = new PlayerResource
                {
                    IsWinner = game.Player2.IsWinner,
                    Name = game.Player2.Name,
                    Score = game.Player2.Score
                },
                HasWinner = game.HasWinner,
                ThrowHistory = game.ThrowHistory.Select(x => new DartThrowResource { Multiplier = x.Multiplier, Value = x.Value, Player = x.Player }).ToList()
            };
        }

        public static GameModelResponse MapToResponse(this GameModel game)
        {
            return new GameModelResponse
            {
                Id = game.Id.ToString(),
                Player1 = new PlayerResponse
                {
                    IsWinner = game.Player1.IsWinner,
                    Name = game.Player1.Name,
                    Score = game.Player1.Score
                },
                Player2 = new PlayerResponse
                {
                    IsWinner = game.Player2.IsWinner,
                    Name = game.Player2.Name,
                    Score = game.Player2.Score
                },
                HasWinner = game.HasWinner,
                ThrowHistory = game.ThrowHistory.Select(x => new DartThrowResponse { Multiplier = x.Multiplier, Value = x.Value, Player = x.Player }).ToList()
            };
        }
    }
}
