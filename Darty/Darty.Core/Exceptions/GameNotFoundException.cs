namespace Darty.Core.Exceptions
{
    using System;

    public class GameNotFoundException : Exception
    {
        public GameNotFoundException(string gameId)
            : base($"Unable to find game with ID: {gameId}")
        {

        }
    }
}
