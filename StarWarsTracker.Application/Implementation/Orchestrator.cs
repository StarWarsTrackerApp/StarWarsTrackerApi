namespace StarWarsTracker.Application.Implementation
{
    internal class Orchestrator : IOrchestrator
    {
        private readonly IHandlerFactory _handlerFactory;

        public Orchestrator(IHandlerFactory handlerFactory) => _handlerFactory = handlerFactory;

        public async Task ExecuteRequestAsync<TRequest>(TRequest request) where TRequest : IRequest
        {
            if (request is IValidatable validatableRequest && !validatableRequest.IsValid(out var validator))
            {
                throw new ValidationFailureException(validator.ReasonsForFailure);
            }

            var handler = _handlerFactory.NewRequestHandler(request);

            await handler.ExecuteRequestAsync(request);
        }

        public async Task<TResponse> GetRequestResponseAsync<TRequest, TResponse>(TRequest request) where TRequest : IRequest<TResponse>
        {
            if (request is IValidatable validatableRequest && !validatableRequest.IsValid(out var validator))
            {
                throw new ValidationFailureException(validator.ReasonsForFailure);
            }

            var handler = _handlerFactory.NewRequestResponseHandler<TRequest, TResponse>(request);

            return await handler.GetResponseAsync(request);
        }
    }
}
