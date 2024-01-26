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

            var handler = _handlerFactory.NewHandler(request);

            await handler.HandleAsync(request);
        }

        public async Task<TResponse> GetRequestResponseAsync<TResponse>(IRequestResponse<TResponse> request)
        {
            if (request is IValidatable validatableRequest && !validatableRequest.IsValid(out var validator))
            {
                throw new ValidationFailureException(validator.ReasonsForFailure);
            }

            var handler = _handlerFactory.NewHandler(request);

            if (await handler.HandleAsync(request) is TResponse response)
            {
                return response;
            }

            throw new OperationFailedException();
        }
    }
}
