using StarWarsTracker.Domain.Logging;

namespace StarWarsTracker.Application.BaseObjects.BaseHandlers
{
    internal abstract class BaseRequestResponseHandler<TRequest, TResponse> : IRequestResponseHandler<TRequest, TResponse> where TRequest : IRequestResponse<TResponse>
    {
        protected readonly ILogMessage _logMessage;

        protected BaseRequestResponseHandler(ILogMessage logMessage)
        {
            _logMessage = logMessage;
        }

        public abstract Task<TResponse> GetResponseAsync(TRequest request);

        public async Task<object?> HandleAsync(object request)
        {
            _logMessage.AddInfo(this, "Getting Request Response", request.GetType().Name);

            var response = await GetResponseAsync((TRequest)request);

            _logMessage.AddInfo(this, "Response Received", response?.GetType().Name);

            return response;
        }
    }
}
