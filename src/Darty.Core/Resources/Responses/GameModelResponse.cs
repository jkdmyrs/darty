namespace Darty.Core.Resources.Responses
{
    using System.Collections.Generic;

    public class GameModelResponse
    {
        public PlayerResponse Player1 { get; set; }
        public PlayerResponse Player2 { get; set; }
        public bool HasWinner { get; set; }
        public string Id { get; set; }
        public List<DartThrowResponse> ThrowHistory { get; set; }
    }
}
