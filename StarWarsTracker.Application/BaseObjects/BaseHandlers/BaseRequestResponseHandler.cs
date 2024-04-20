using StarWarsTracker.Logging.Abstraction;

namespace StarWarsTracker.Application.BaseObjects.BaseHandlers
{
    /// <summary>
    /// This base class is used for Handlers that will get a response for an IRequestResponse.
    /// </summary>
    /// <typeparam name="TRequest">The type of IRequestResponse to get a response for.</typeparam>
    /// <typeparam name="TResponse">The type of response that will be returned by the handler. This type is defined by the IRequestResponse</typeparam>
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
