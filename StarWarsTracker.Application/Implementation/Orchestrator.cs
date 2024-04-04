using StarWarsTracker.Domain.Logging;

namespace StarWarsTracker.Application.Implementation
{
    internal class Orchestrator : IOrchestrator
    {
        #region Private Members

        private readonly IHandlerFactory _handlerFactory;

        private readonly ILogMessage _logMessage;

        #endregion

        #region Constructors

        public Orchestrator(IHandlerFactory handlerFactory, ILogMessage logMessage)
        {
            _handlerFactory = handlerFactory;

            _logMessage = logMessage;
        }

        #endregion

        #region Public Methods 

        public async Task ExecuteRequestAsync<TRequest>(TRequest request) where TRequest : IRequest
        {
            _logMessage.AddInfo(this, "Orchestrator Receiving Request", request.GetType().Name);

            if (request is IValidatable validatableRequest && !validatableRequest.IsValid(out var validator))
            {
                throw new ValidationFailureException(validator.ReasonsForFailure);
            }

            var handler = _handlerFactory.NewHandler(request);

            _logMessage.AddDebug(this, "Handler Instantiated", handler.GetType().Name);

            await handler.HandleAsync(request);

            _logMessage.AddInfo(this, "Request Handled");
        }

        public async Task<TResponse> GetRequestResponseAsync<TResponse>(IRequestResponse<TResponse> request)
        {
            _logMessage.AddInfo(this, "Orchestrator Receiving Request", request.GetType().Name);

            if (request is IValidatable validatableRequest && !validatableRequest.IsValid(out var validator))
            {
                throw new ValidationFailureException(validator.ReasonsForFailure);
            }

            var handler = _handlerFactory.NewHandler(request);

            _logMessage.AddDebug(this, "Handler Instantiated", handler.GetType().Name);

            var result = await handler.HandleAsync(request);

            _logMessage.AddDebug(this, "Handler Response", result);

            if (result is TResponse response)
            {
                _logMessage.AddInfo(this, "Handler Returned Expected Response Type.", typeof(TResponse).Name);

                return response;
            }

            _logMessage.IncreaseLevel(LogLevel.Critical, this, "Handler Returned Unexpected Response Type");

            throw new OperationFailedException();
        }

        #endregion
    }
}
