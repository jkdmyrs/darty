namespace Darty.API.Common
{
    using Microsoft.AspNetCore.Mvc;

    class InternalServerErrorObjectResult : ObjectResult
    {
        public InternalServerErrorObjectResult() : base(new DartyAPIError("500", "An unknown error occured."))
        {
            this.StatusCode = 500;
        }
    }
}
