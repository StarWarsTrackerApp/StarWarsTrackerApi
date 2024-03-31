using StarWarsTracker.Domain.Logging;

namespace StarWarsTracker.Application.Implementation
{
    internal class Orchestrator : IOrchestrator
    {
        #region Private Members

        private readonly IHandlerFactory _handlerFactory;

        private readonly ILogger<Orchestrator> _logger;

        #endregion

        #region Constructors

        public Orchestrator(IHandlerFactory handlerFactory, ILoggerFactory loggerFactory)
        {
            _handlerFactory = handlerFactory;

            _logger = loggerFactory.NewLogger<Orchestrator>();
        }

        #endregion

        #region Public Methods 

        public async Task ExecuteRequestAsync<TRequest>(TRequest request) where TRequest : IRequest
        {
            var logMessage = LogMessage.New();

            logMessage.AddTrace("Orchestrator Receiving Request", request.GetType().Name);
            logMessage.AddTrace("Request Body", request);

            if (request is IValidatable validatableRequest && !validatableRequest.IsValid(out var validator))
            {
                logMessage.AddDebug("Request Validation Failed", validator.ReasonsForFailure);

                _logger.LogDebug(logMessage);

                throw new ValidationFailureException(validator.ReasonsForFailure);
            }

            var handler = _handlerFactory.NewHandler(request);

            logMessage.AddTrace("Handler Instantiated", handler.GetType().Name);

            await handler.HandleAsync(request);

            logMessage.AddTrace("Request Handled");

            _logger.LogTrace(logMessage);
        }

        public async Task<TResponse> GetRequestResponseAsync<TResponse>(IRequestResponse<TResponse> request)
        {
            var logMessage = LogMessage.New();

            logMessage.AddTrace("Orchestrator Receiving Request", request.GetType().Name);
            logMessage.AddTrace("Request Body", request);

            if (request is IValidatable validatableRequest && !validatableRequest.IsValid(out var validator))
            {
                logMessage.AddDebug("Request Validation Failed", validator.ReasonsForFailure);

                _logger.LogDebug(logMessage);

                throw new ValidationFailureException(validator.ReasonsForFailure);
            }

            var handler = _handlerFactory.NewHandler(request);

            logMessage.AddTrace("Handler Instantiated", handler.GetType().Name);

            var result = await handler.HandleAsync(request);

            logMessage.AddTrace("Handler Response", result);

            if (result is TResponse response)
            {
                _logger.LogTrace(logMessage);

                return response;
            }

            logMessage.AddCritical("Handler Returned Unexpected Response Type");

            _logger.LogCritical(logMessage);

            throw new OperationFailedException();
        }

        #endregion
    }
}
