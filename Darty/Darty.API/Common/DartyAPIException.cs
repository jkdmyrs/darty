namespace Darty.API.Common
{
    using System;

    class DartyAPIException : Exception
    {
        public DartyAPIError Error { get; set; }
        public int Code { get; set; }

        public DartyAPIException(int code, string message)
            : base(message)
        {
            this.Error = new DartyAPIError(code.ToString(), message);
            this.Code = code;
        }
    }
}
