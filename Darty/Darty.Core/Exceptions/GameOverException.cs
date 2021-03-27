namespace Darty.Core.Exceptions
{
    using System;

    public class GameOverException : Exception
    {
        public GameOverException(string gameId)
            : base($"Cannot change game `{gameId}` because the game already has a winner.")
        {

        }
    }
}
