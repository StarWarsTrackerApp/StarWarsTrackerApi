using StarWarsTracker.Application.Abstraction;
using StarWarsTracker.Domain.Exceptions;
using StarWarsTracker.Domain.Validation;

namespace StarWarsTracker.Application.Implementation
{
    internal class Orchestrator : IOrchestrator
    {
        private readonly IHandlerFactory _handlerFactory;

        public Orchestrator(IHandlerFactory handlerFactory) => _handlerFactory = handlerFactory;

        public async Task SendRequest<TRequest>(TRequest request) where TRequest : IRequest
        {
            if (request is IValidatable validatableRequest && !validatableRequest.IsValid(out var validator))
            {
                throw new ValidationFailureException(validator.ReasonsForFailure);
            }

            var handler = _handlerFactory.NewRequestHandler(request);

            await handler.HandleRequestAsync(request);
        }

        public async Task<TResponse> SendRequest<TRequest, TResponse>(TRequest request) where TRequest : IRequest<TResponse>
        {
            if (request is IValidatable validatableRequest && !validatableRequest.IsValid(out var validator))
            {
                throw new ValidationFailureException(validator.ReasonsForFailure);
            }

            var handler = _handlerFactory.NewRequestHandler<TRequest, TResponse>(request);

            return await handler.HandleRequestAsync(request);
        }
    }
}
