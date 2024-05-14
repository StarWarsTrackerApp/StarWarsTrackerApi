using StarWarsTracker.Logging.Abstraction;

namespace StarWarsTracker.Application.BaseObjects.BaseHandlers
{
    /// <summary>
    /// This base class adds a Logger for all Handlers that inherit from it. 
    /// </summary>
    /// <typeparam name="TRequest">The Type of IRequest to be executed.</typeparam>
    internal abstract class BaseHandler<TRequest> : IHandler<TRequest>
    {
        protected readonly IClassLogger _logger;

        internal protected BaseHandler(IClassLoggerFactory loggerFactory) => _logger = loggerFactory.GetLoggerFor(this);

        internal protected abstract Task<IResponse> HandleRequestAsync(TRequest request);

        public async Task<IResponse> GetResponseAsync(TRequest request)
        {
            _logger.AddInfo($"Receiving Request: {request?.GetType().Name}");

            var response = await HandleRequestAsync(request);

            _logger.AddInfo($"Request Handled, Response: {response.GetType().Name}");

            return response;
        }
    }
}
