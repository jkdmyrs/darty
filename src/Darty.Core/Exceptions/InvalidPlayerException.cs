namespace Darty.Core.Exceptions
{
    using System;

    public class InvalidPlayerException : Exception
    {
        public InvalidPlayerException(string player)
            : base($"Player `{player}` is not valid in the current game.")
        {

        }
    }
}
