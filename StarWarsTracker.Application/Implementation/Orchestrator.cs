using StarWarsTracker.Application.Abstraction;

namespace StarWarsTracker.Application.Implementation
{
    internal class Orchestrator : IOrchestrator
    {
        private readonly IHandlerFactory _handlerFactory;

        public Orchestrator(IHandlerFactory handlerFactory) => _handlerFactory = handlerFactory;

        public async Task SendRequest<TRequest>(TRequest request) where TRequest : IRequest
        {
            var handler = _handlerFactory.NewRequestHandler(request);

            await handler.HandleRequestAsync(request);
        }

        public async Task<TResponse> SendRequest<TRequest, TResponse>(TRequest request) where TRequest : IRequest<TResponse>
        {
            var handler = _handlerFactory.NewRequestHandler<TRequest, TResponse>(request);

            return await handler.HandleRequestAsync(request);
        }
    }
}
