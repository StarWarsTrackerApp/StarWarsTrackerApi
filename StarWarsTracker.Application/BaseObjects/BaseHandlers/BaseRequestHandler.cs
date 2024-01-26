namespace StarWarsTracker.Application.BaseObjects.BaseHandlers
{
    internal abstract class BaseRequestHandler<TRequest> : IRequestHandler<TRequest> where TRequest : IRequest
    {
        public abstract Task ExecuteRequestAsync(TRequest request);

        public async Task<object?> HandleAsync(object request)
        {
            await ExecuteRequestAsync((TRequest)request);

            return null;
        }
    }
}
