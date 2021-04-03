namespace jkdmyrs.Functions.Functions
{
    using Darty.API.Constants;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Http;

    public class HealthCheckFunctions
    {
        [FunctionName(FunctionNames.HealthCheck)]
        public IActionResult RunHealthCheck(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "health")] HttpRequest req)
        {
            return new NoContentResult();
        }
    }
}
