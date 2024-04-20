namespace StarWarsTracker.Application.Implementation
{
    /// <summary>
    /// This class implements the IHandlerDictionary interface by using a Dictionary (of Type, Type).
    /// The Key is the Type of IRequest or IRequestResponse. The Value is the Type of Handler that will handle the Request.
    /// </summary>
    internal class HandlerDictionary : IHandlerDictionary
    {
        #region Private Members

        private readonly Dictionary<Type, Type> _handlersDictionary;

        #endregion

        #region Constructors

        /// <summary>
        /// Default Constructor will instantiate HandlersDictionary with all Handlers from the StarWarsTracker.Application project.
        /// </summary>
        public HandlerDictionary()
        {
            // get Types that implement IBaseHandler from this assembly.
            var handlers = GetType().Assembly.GetTypes().Where(x => typeof(IBaseHandler).IsAssignableFrom(x) && x.IsClass);

            _handlersDictionary = GetHandlersDictionary(handlers);
        }

        /// <summary>
        /// This constructor takes in an IEnumerable of Types of IHandler's which will be added to the HandlerDictionary.
        /// </summary>
        public HandlerDictionary(IEnumerable<Type> handlers) => _handlersDictionary = GetHandlersDictionary(handlers);

        #endregion

        #region Public IHandlerDictionary Method

        public Type? GetHandlerType(Type requestType) => _handlersDictionary.TryGetValue(requestType, out var result) ? result : null;

        #endregion

        #region Private Helper Method
        
        /// <summary>
        /// Helper to transform collection of Types to a Dictionary with the first generic's type from the first interface that has generic types. 
        /// This is looking for the IRequest type for the IRequestHandler - if the IRequest is not found, throws a DoesNotExistException.
        /// </summary>
        private static Dictionary<Type, Type> GetHandlersDictionary(IEnumerable<Type> handlers) =>
            handlers.ToDictionary(_ => _.GetInterfaces()
                                        .FirstOrDefault(_ => _.GenericTypeArguments.Any())?
                                        .GenericTypeArguments.First()
                                        ?? throw new DoesNotExistException("RequestObject", (_, _.Name)));
        
        #endregion
    }
}
