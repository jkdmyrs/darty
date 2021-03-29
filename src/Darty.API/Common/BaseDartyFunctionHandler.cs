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
                T result = await work.ConfigureAwait(false);
                return new OkObjectResult(result);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        public async Task<IActionResult> RunAsync(Task work)
        {
            try
            {
                await work.ConfigureAwait(false);
                return new OkResult();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        private IActionResult HandleException(Exception e)
        {
            if (TryHandleCoreException(e, out Exception newException))
            {
                DartyAPIException handledException = newException as DartyAPIException;
                _logger.LogWarning($"Known error was handled. StatusCode: {handledException.Error.Code}, Message: {handledException.Error.Message}");
                // handle known errors
                return new ObjectResult(handledException.Error)
                {
                    StatusCode = handledException.Code
                };
            }
            else
            {
                _logger.LogError($"Unknown error. Message: {e.Message}");
                // 500 on unknown errors
                return new InternalServerErrorObjectResult();
            }
        }

        private bool TryHandleCoreException(Exception maybeCoreException, out Exception e)
        {
            if (maybeCoreException is NotImplementedException notImplemented)
            {
                e = new DartyAPIException(StatusCodes.Status501NotImplemented, notImplemented.Message);
            }
            else if (maybeCoreException is GameNotFoundException gameNotFound)
            {
                e = new DartyAPIException(StatusCodes.Status404NotFound, gameNotFound.Message);
            }
            else if (maybeCoreException is InvalidPlayerException invalidPlayer)
            {
                e = new DartyAPIException(StatusCodes.Status400BadRequest, invalidPlayer.Message);
            }
            else if (maybeCoreException is GameOverException gameOver)
            {
                e = new DartyAPIException(StatusCodes.Status409Conflict, gameOver.Message);
            }
            else if (maybeCoreException is InvalidDartMultiplierException invalidMultiplier)
            {
                e = new DartyAPIException(StatusCodes.Status400BadRequest, invalidMultiplier.Message);
            }
            else if (maybeCoreException is InvalidDartValueException invalidValue)
            {
                e = new DartyAPIException(StatusCodes.Status400BadRequest, invalidValue.Message);
            }
            else
            {
                e = maybeCoreException;
                // was not handle-able
                return false;
            }
            // was handled
            return true;
        }
    }
}
