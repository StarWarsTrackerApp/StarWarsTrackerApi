using StarWarsTracker.Logging.Abstraction;

namespace StarWarsTracker.Application.Implementation
{
    /// <summary>
    /// This class implements the IHandleFactory in order to instantiate a new Handler at runtime.
    /// The Type of Handler to instantiate is determined by sending the TRequest to the IHandlerDictionary.
    /// The Handler is instantiated by using the ITypeActivator.
    /// </summary>
    internal class HandlerFactory : IHandlerFactory
    {
        #region Private Members

        private readonly IHandlerDictionary _handlerDictionary;

        private readonly ITypeActivator _typeActivator;

        private readonly IClassLogger _logger;

        #endregion

        #region Constructor

        public HandlerFactory(ITypeActivator typeActivator, IHandlerDictionary handlers, IClassLoggerFactory classLoggerFactory)
        {
            _typeActivator = typeActivator;

            _handlerDictionary = handlers;

            _logger = classLoggerFactory.GetLoggerFor(this);
        }

        #endregion

        #region Public Method

        public IBaseHandler NewHandler<TRequest>(TRequest request)
        {
            _logger.AddInfo("Handler Factory Receiving Request", request?.GetType().Name);

            if (request is null)
            {
                var exception = new ArgumentNullException(nameof(request));

                _logger.IncreaseLevel(LogLevel.Critical, "Attempted To Instantiate Handler For Null TRequest");

                throw exception;
            }

            _logger.AddTrace("Looking for Handler", request.GetType().Name);
            
            var handlerType = _handlerDictionary.GetHandlerType(request.GetType());            

            if (handlerType == null)
            {
                _logger.IncreaseLevel(LogLevel.Critical, "No Handler Found", request.GetType().Name);

                throw new DoesNotExistException("RequestHandler", (request, nameof(request)));
            }

            _logger.AddTrace("Located Handler Type", handlerType.Name);

            var handler = _typeActivator.Instantiate<IBaseHandler>(handlerType);

            _logger.AddInfo("Instantiated Handler", handlerType.Name);

            return handler;
        }
        
        #endregion
    }
}
