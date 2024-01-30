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

        public IBaseHandler NewHandler<TRequest>(TRequest request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request)); 
            }

            var handlerType = _handlers.GetHandlerType(request.GetType());

            if (handlerType == null)
            {
                throw new DoesNotExistException("RequestHandler", (request, nameof(request)));
            }

            return _typeActivator.Instantiate<IBaseHandler>(handlerType);
        }
    }
}
