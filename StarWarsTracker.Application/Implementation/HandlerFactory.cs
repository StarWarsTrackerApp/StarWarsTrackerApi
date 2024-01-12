using Microsoft.Extensions.DependencyInjection;
using StarWarsTracker.Application.Abstraction;
using StarWarsTracker.Domain.Exceptions;

namespace StarWarsTracker.Application.Implementation
{
    internal class HandlerFactory : IHandlerFactory
    {
        private readonly IHandlerDictionary _handlers;

        private readonly IServiceProvider _serviceProvider;

        public HandlerFactory(IServiceProvider serviceProvider, IHandlerDictionary handlers)
        {
            _serviceProvider = serviceProvider;

            _handlers = handlers;
        }

        public IRequestHandler<TRequest> NewRequestHandler<TRequest>(TRequest request) where TRequest : IRequest
        {
            var handlerType = _handlers.GetHandlerType(request.GetType());

            if (handlerType == null)
            {
                throw new DoesNotExistException(nameof(IRequestHandler<TRequest>), (request, nameof(request)));
            }

            return (IRequestHandler<TRequest>)ActivatorUtilities.CreateInstance(_serviceProvider, handlerType);
        }

        public IRequestHandler<TRequest, TResponse> NewRequestHandler<TRequest, TResponse>(TRequest request) where TRequest : IRequest<TResponse>
        {
            var handlerType = _handlers.GetHandlerType(request.GetType());

            if (handlerType == null)
            {
                throw new DoesNotExistException(nameof(IRequestHandler<TRequest, TResponse>), (request, nameof(request)));
            }

            return (IRequestHandler<TRequest, TResponse>)ActivatorUtilities.CreateInstance(_serviceProvider, handlerType);
        }
    }
}
