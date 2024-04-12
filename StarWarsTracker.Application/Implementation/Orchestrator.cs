using StarWarsTracker.Logging.Abstraction;

namespace StarWarsTracker.Application.Implementation
{
    internal class Orchestrator : IOrchestrator
    {
        #region Private Members

        private readonly IHandlerFactory _handlerFactory;

        private readonly IClassLogger _logger;

        #endregion

        #region Constructors

        public Orchestrator(IHandlerFactory handlerFactory, IClassLoggerFactory loggerFactory)
        {
            _handlerFactory = handlerFactory;

            _logger = loggerFactory.GetLoggerFor(this);
        }

        #endregion

        #region Public Methods 

        public async Task ExecuteRequestAsync<TRequest>(TRequest request) where TRequest : IRequest
        {
            _logger.AddInfo("Orchestrator Receiving Request", request.GetType().Name);

            if (request is IValidatable validatableRequest && !validatableRequest.IsValid(out var validator))
            {
                throw new ValidationFailureException(validator.ReasonsForFailure);
            }

            var handler = _handlerFactory.NewHandler(request);

            _logger.AddDebug("Handler Instantiated", handler.GetType().Name);

            await handler.HandleAsync(request);

            _logger.AddInfo("Request Handled");
        }

        public async Task<TResponse> GetRequestResponseAsync<TResponse>(IRequestResponse<TResponse> request)
        {
            _logger.AddInfo("Orchestrator Receiving Request", request.GetType().Name);

            if (request is IValidatable validatableRequest && !validatableRequest.IsValid(out var validator))
            {
                throw new ValidationFailureException(validator.ReasonsForFailure);
            }

            var handler = _handlerFactory.NewHandler(request);

            _logger.AddDebug("Handler Instantiated", handler.GetType().Name);

            var result = await handler.HandleAsync(request);

            _logger.AddDebug("Handler Response", result);

            if (result is TResponse response)
            {
                _logger.AddInfo("Handler Returned Expected Response Type.", typeof(TResponse).Name);

                return response;
            }

            _logger.IncreaseLevel(LogLevel.Critical, "Handler Returned Unexpected Response Type");

            throw new OperationFailedException();
        }

        #endregion
    }
}
