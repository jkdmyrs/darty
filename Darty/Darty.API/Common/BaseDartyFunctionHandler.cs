namespace Darty.API.Common
{
    using Darty.Core.Exceptions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Threading.Tasks;

    public abstract class BaseDartyFunctionHandler
    {
        private readonly ILogger _logger;

        public BaseDartyFunctionHandler(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IActionResult> RunAsync<T>(Task<T> work)
        {
            try
            {
                // do work and return
                var result = await this.HandleCoreExceptions(work).ConfigureAwait(false);
                return new OkObjectResult(result);
            }
            catch (DartyAPIException knownError)
            {
                _logger.LogWarning($"Known error was handled. StatusCode: {knownError.Error.Code}, Message: {knownError.Error.Message}");
                // handle known errors
                return new ObjectResult(knownError.Error)
                {
                    StatusCode = knownError.Code
                };
            }
            catch (Exception e)
            {
                _logger.LogError($"Unknown error. Message: {e.Message}");
                // 500 on unknown errors
                return new InternalServerErrorObjectResult();
            }
        }

        /// <summary>
        /// Converts an Exception from Darty.Core to a DartyException.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="work"></param>
        /// <returns></returns>
        private async Task<T> HandleCoreExceptions<T>(Task<T> work)
        {
            try
            {
                return await work.ConfigureAwait(false);
            }
            catch (GameNotFoundException gameNotFound)
            {
                throw new DartyAPIException(StatusCodes.Status404NotFound, gameNotFound.Message);
            }
            catch (InvalidPlayerException invalidPlayer)
            {
                throw new DartyAPIException(StatusCodes.Status400BadRequest, invalidPlayer.Message);
            }
        }
    }
}
