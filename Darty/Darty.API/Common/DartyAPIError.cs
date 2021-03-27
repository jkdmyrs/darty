namespace Darty.API.Common
{
    public class DartyAPIError
    {
        public string Code { get; set; }
        public string Message { get; set; }

        public DartyAPIError(string code, string message)
        {
            Code = code;
            Message = message;
        }
    }
}
