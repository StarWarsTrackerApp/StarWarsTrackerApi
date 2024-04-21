using StarWarsTracker.Logging.Abstraction;

namespace StarWarsTracker.Application.Implementation
{
    /// <summary>
    /// This class implements the IOrchestrator interface in order to handle IRequest and IRequestResponse requests.
    /// The Orchestrator will validate the request if it implements IValidatable.
    /// The 
    /// </summary>
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

        #region Public IOrchestrator Methods 

        public async Task ExecuteRequestAsync<TRequest>(TRequest request) where TRequest : IRequest
        {
            _logger.AddInfo("Orchestrator Receiving Request", request.GetType().Name);

            ValidateRequest(request);

            var handler = _handlerFactory.NewHandler(request);

            _logger.AddTrace("Handler Instantiated", handler);

            await handler.HandleAsync(request);

            _logger.AddInfo("Request Handled");
        }

        public async Task<TResponse> GetRequestResponseAsync<TResponse>(IRequestResponse<TResponse> request)
        {
            _logger.AddInfo("Orchestrator Receiving Request", request.GetType().Name);

            ValidateRequest(request);

            var handler = _handlerFactory.NewHandler(request);

            _logger.AddTrace("Handler Instantiated", handler);

            var result = await handler.HandleAsync(request);

            _logger.AddTrace("Handler Response", result);

            if (result is TResponse response)
            {
                _logger.AddInfo("Handler Returned Expected Response Type.", typeof(TResponse).Name);

                return response;
            }

            _logger.IncreaseLevel(LogLevel.Critical, "Handler Returned Unexpected Response Type");

            throw new OperationFailedException();
        }

        #endregion

        #region Private Helper Methods

        /// <summary>
        /// Helper to validate request and implement logging around Request Validation.
        /// Throws ValidationFailureException if Request Validation Fails.
        /// </summary>
        /// <typeparam name="TRequest">The Type of Request to Validate</typeparam>
        /// <param name="request">The Request being Validated</param>
        /// <exception cref="ValidationFailureException"></exception>
        private void ValidateRequest<TRequest>(TRequest request)
        {
            if (request is IValidatable validatableRequest)
            {
                if (!validatableRequest.IsValid(out var validator))
                {
                    _logger.AddTrace("Request Failed Validation", validator.ReasonsForFailure);

                    throw new ValidationFailureException(validator.ReasonsForFailure);
                }

                _logger.AddTrace("Request Passed Validation");
            }
            else
            {
                _logger.AddTrace("Request Is Not IValidatable");
            }
        }

        #endregion
    }
}
