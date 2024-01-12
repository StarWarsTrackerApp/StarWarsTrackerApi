namespace StarWarsTracker.Application.Abstraction
{
    public interface IOrchestrator
    {
        public Task SendRequest<TRequest>(TRequest request) where TRequest : IRequest;

        public Task<TResponse> SendRequest<TRequest, TResponse>(TRequest request) where TRequest : IRequest<TResponse>;
    }
}
