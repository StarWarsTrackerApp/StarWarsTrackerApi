namespace StarWarsTracker.Application.Abstraction
{
    internal interface IHandlerFactory
    {
        public IRequestHandler<TRequest> NewRequestHandler<TRequest>(TRequest request) where TRequest : IRequest;

        public IRequestHandler<TRequest, TResponse> NewRequestHandler<TRequest, TResponse>(TRequest request) where TRequest : IRequest<TResponse>;
    }
}
