using StarWarsTracker.Domain.Logging;

namespace StarWarsTracker.Application.BaseObjects.BaseHandlers
{
    internal abstract class BaseRequestHandler<TRequest> : IRequestHandler<TRequest> where TRequest : IRequest
    {
        protected readonly ILogMessage _logMessage;

        protected BaseRequestHandler(ILogMessage logMessage)
        {
            _logMessage = logMessage;
        }

        public abstract Task ExecuteRequestAsync(TRequest request);

        public async Task<object?> HandleAsync(object request)
        {
            _logMessage.AddInfo(this, "Handling Request", request.GetType().Name);

            await ExecuteRequestAsync((TRequest)request);

            _logMessage.AddInfo(this, "Request Handled");

            return null;
        }
    }
}
