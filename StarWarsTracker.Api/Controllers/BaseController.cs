using StarWarsTracker.Domain.Constants.LogConfigs;
using StarWarsTracker.Domain.Validation;
using StarWarsTracker.Logging.Abstraction;

namespace StarWarsTracker.Api.Controllers
{
    public abstract class BaseController
    {
        #region Private Members

        private readonly IHandlerFactory _handlerFactory;

        private readonly IClassLogger _logger;

        #endregion

        #region Constructor

        public BaseController(IHandlerFactory handlerFactory, IClassLoggerFactory loggerFactory)
        {
            _handlerFactory = handlerFactory;

            _logger = loggerFactory.GetLoggerFor(this);
        }

        #endregion

        #region Protected Methods

        protected async Task<IActionResult> HandleAsync<TRequest>(TRequest request) where TRequest : class
        {
            var response = await GetResponseAsync(request);

            var statusCode = response.GetStatusCode();

            var responseBody = response.GetBody();

            return responseBody == null ? new StatusCodeResult(statusCode) : new ObjectResult(responseBody) { StatusCode = statusCode };
        }

        internal protected async Task<IResponse> GetResponseAsync<TRequest>(TRequest request) where TRequest : class
        {
            if (request == null)
            {
                return Response.ValidationFailure("Request Is NULL");
            }

            _logger.AddConfiguredLogLevel(Section.ControllerLogging, Key.ControllerRequestBodyLogLevel, $"Request Received: {request.GetType().Name}", request);

            if (request is IValidatable validatable && !validatable.IsValid(out var validator))
            {
                return Response.ValidationFailure(validator.ReasonsForFailure);
            }

            var handler = _handlerFactory.GetHandler(request);

            var response = await handler.GetResponseAsync(request);

            _logger.AddConfiguredLogLevel(Section.ControllerLogging, Key.ControllerResponseBodyLogLevel, $"Response Received: ", response);

            return response;
        }

        #endregion
    }
}
