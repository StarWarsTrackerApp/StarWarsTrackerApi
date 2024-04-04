using StarWarsTracker.Domain.Logging;

namespace StarWarsTracker.Application.Implementation
{
    internal class HandlerFactory : IHandlerFactory
    {
        #region Private Members

        private readonly IHandlerDictionary _handlerDictionary;

        private readonly ITypeActivator _typeActivator;

        private readonly ILogMessage _logMessage;

        #endregion

        #region Constructor

        public HandlerFactory(ITypeActivator typeActivator, IHandlerDictionary handlers, ILogMessage logMessage)
        {
            _typeActivator = typeActivator;

            _handlerDictionary = handlers;

            _logMessage = logMessage;
        }

        #endregion

        #region Public Method

        public IBaseHandler NewHandler<TRequest>(TRequest request)
        {
            _logMessage.AddInfo(this, "Handler Factory Receiving Request", request?.GetType().Name);

            if (request is null)
            {
                var exception = new ArgumentNullException(nameof(request));

                _logMessage.IncreaseLevel(LogLevel.Critical, this, "Attempted To Instantiate Handler For Null TRequest");

                throw exception;
            }

            _logMessage.AddTrace(this, "Looking for Handler", request.GetType().Name);
            
            var handlerType = _handlerDictionary.GetHandlerType(request.GetType());            

            if (handlerType == null)
            {
                _logMessage.IncreaseLevel(LogLevel.Critical, this, $"No Handler Found", request.GetType().Name);

                throw new DoesNotExistException("RequestHandler", (request, nameof(request)));
            }

            _logMessage.AddTrace(this, "Located Handler", handlerType.Name);

            var handler = _typeActivator.Instantiate<IBaseHandler>(handlerType);

            _logMessage.AddInfo(this, "Instantiated Handler", handlerType.Name);

            return handler;
        }
        
        #endregion
    }
}
