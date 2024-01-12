namespace StarWarsTracker.Application.Abstraction
{
    internal interface IBaseHandler { }

    internal interface IRequestHandler<TRequest> : IBaseHandler where TRequest : IRequest
    {
        public Task HandleRequestAsync(TRequest request);
    }

    internal interface IRequestHandler<TRequest, TResponse> : IBaseHandler where TRequest : IRequest<TResponse>
    {
        public Task<TResponse> HandleRequestAsync(TRequest request);
    }
}
