using StarWarsTracker.Domain.Logging;

namespace StarWarsTracker.Application.Implementation
{
    internal class HandlerFactory : IHandlerFactory
    {
        #region Private Members

        private readonly IHandlerDictionary _handlerDictionary;

        private readonly ITypeActivator _typeActivator;

        private readonly ILogger<HandlerFactory> _logger;

        #endregion

        #region Constructor

        public HandlerFactory(ITypeActivator typeActivator, IHandlerDictionary handlers, ILoggerFactory loggerFactory)
        {
            _typeActivator = typeActivator;

            _handlerDictionary = handlers;

            _logger = loggerFactory.NewLogger<HandlerFactory>();
        }

        #endregion

        #region Public Method

        public IBaseHandler NewHandler<TRequest>(TRequest request)
        {
            var logMessage = LogMessage.New();

            logMessage.AddTrace("Handler Factory Receiving Request", request);

            if (request is null)
            {
                var exception = new ArgumentNullException(nameof(request));

                logMessage.AddCritical("Attempted To Instantiate Handler For Null TRequest", exception);

                _logger.LogCritical(logMessage, exception.StackTrace);

                throw exception;
            }

            logMessage.AddTrace("Looking for Handler", request.GetType().Name);
            
            var handlerType = _handlerDictionary.GetHandlerType(request.GetType());            

            if (handlerType == null)
            {
                var exception = new DoesNotExistException("RequestHandler", (request, nameof(request)));

                logMessage.AddCritical($"No Handler Found", request.GetType().Name);

                _logger.LogCritical(logMessage, exception.StackTrace);

                throw exception;
            }

            logMessage.AddTrace("Located Handler", handlerType.Name);

            var handler = _typeActivator.Instantiate<IBaseHandler>(handlerType);

            logMessage.AddTrace("Instantiated Handler", handler);

            _logger.LogTrace(logMessage);

            return handler;
        }
        
        #endregion
    }
}
