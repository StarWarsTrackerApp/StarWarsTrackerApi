namespace StarWarsTracker.Application.Implementation
{
    internal class HandlerFactory : IHandlerFactory
    {
        private readonly IHandlerDictionary _handlers;

        private readonly ITypeActivator _typeActivator;

        public HandlerFactory(ITypeActivator typeActivator, IHandlerDictionary handlers)
        {
            _typeActivator = typeActivator;

            _handlers = handlers;
        }

        public IRequestHandler<TRequest> NewRequestHandler<TRequest>(TRequest request) where TRequest : IRequest
        {
            var handlerType = _handlers.GetHandlerType(request.GetType());

            if (handlerType == null)
            {
                throw new DoesNotExistException(nameof(IRequestHandler<TRequest>), (request, nameof(request)));
            }

            return _typeActivator.Instantiate<IRequestHandler<TRequest>>(handlerType);
        }

        public IRequestResponseHandler<TRequest, TResponse> NewRequestResponseHandler<TRequest, TResponse>(TRequest request) where TRequest : IRequestResponse<TResponse>
        {
            var handlerType = _handlers.GetHandlerType(request.GetType());

            if (handlerType == null)
            {
                throw new DoesNotExistException(nameof(IRequestResponseHandler<TRequest, TResponse>), (request, nameof(request)));
            }

            return _typeActivator.Instantiate<IRequestResponseHandler<TRequest, TResponse>>(handlerType);
        }
    }
}
