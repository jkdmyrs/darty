namespace Darty.Core.Exceptions
{
    using System;

    public class InvalidDartValueException : Exception
    {
        public InvalidDartValueException(string value)
            : base($"Invalid dart value `{value}`.")
        {

        }
    }
}
