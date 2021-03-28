namespace Darty.Core.Resources.Data
{
    using System.Collections.Generic;

    public class GameModelResource
    {
        public PlayerResource Player1 { get; set; }
        public PlayerResource Player2 { get; set; }
        public bool HasWinner { get; set; }
        public string Id { get; set; }
        public List<DartThrowResource> ThrowHistory { get; set; }
    }
}
