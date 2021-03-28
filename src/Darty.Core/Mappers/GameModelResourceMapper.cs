namespace Darty.Core.Mappers
{
    using Darty.Core.Models;
    using Darty.Core.Resources.Data;
    using System.Linq;

    public static class GameModelResourceMapper
    {
        public static GameModel MapToModel(this GameModelResource game)
        {
            return new GameModel(
                game.Player1.Name,
                game.Player2.Name,
                game.ThrowHistory.Select(x => (x.Value, x.Multiplier, x.Player)).ToList(),
                game.Id);
        }
    }
}
