namespace Darty.Core.Exceptions
{
    using System;

    public class InvalidDartMultiplierException : Exception
    {
        public InvalidDartMultiplierException(string value)
            : base($"Invalid dart multiplier `{value}`.")
        {

        }
    }
}
