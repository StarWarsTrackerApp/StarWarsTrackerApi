using StarWarsTracker.Logging.Abstraction;

namespace StarWarsTracker.Application.BaseObjects.BaseHandlers
{
    internal abstract class BaseRequestResponseHandler<TRequest, TResponse> : IRequestResponseHandler<TRequest, TResponse> where TRequest : IRequestResponse<TResponse>
    {
        protected readonly IClassLogger _logger;

        protected BaseRequestResponseHandler(IClassLoggerFactory loggerFactory)
        {
            _logger = loggerFactory.GetLoggerFor(this);
        }

        public abstract Task<TResponse> GetResponseAsync(TRequest request);

        public async Task<object?> HandleAsync(object request)
        {
            _logger.AddInfo("Getting Request Response", request.GetType().Name);

            var response = await GetResponseAsync((TRequest)request);

            _logger.AddInfo("Response Received", response?.GetType().Name);

            return response;
        }
    }
}
