using StarWarsTracker.Logging.Abstraction;

namespace StarWarsTracker.Application.BaseObjects.BaseHandlers
{
    internal abstract class BaseRequestHandler<TRequest> : IRequestHandler<TRequest> where TRequest : IRequest
    {
        protected readonly IClassLogger _logger;

        protected BaseRequestHandler(IClassLoggerFactory loggerFactory)
        {
            _logger = loggerFactory.GetLoggerFor(this);
        }

        public abstract Task ExecuteRequestAsync(TRequest request);

        public async Task<object?> HandleAsync(object request)
        {
            _logger.AddInfo("Handling Request", request.GetType().Name);

            await ExecuteRequestAsync((TRequest)request);

            _logger.AddInfo("Request Handled");

            return null;
        }
    }
}
