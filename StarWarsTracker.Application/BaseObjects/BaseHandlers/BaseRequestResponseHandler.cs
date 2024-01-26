namespace StarWarsTracker.Application.BaseObjects.BaseHandlers
{
    internal abstract class BaseRequestResponseHandler<TRequest, TResponse> : IRequestResponseHandler<TRequest, TResponse> where TRequest : IRequestResponse<TResponse>
    {
        public abstract Task<TResponse> GetResponseAsync(TRequest request);

        public async Task<object?> HandleAsync(object request) => await GetResponseAsync((TRequest)request);
    }
}
