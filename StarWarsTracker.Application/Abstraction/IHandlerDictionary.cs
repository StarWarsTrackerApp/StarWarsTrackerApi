namespace StarWarsTracker.Application.Abstraction
{
    /// <summary>
    /// This interface defines the contract for the Handler Dictionary which will use RequestTypes to locate and return the appropriate IHandler.
    /// </summary>
    public interface IHandlerDictionary
    {
        /// <summary>
        /// Try to get the Type of handler that handles the TRequest provided. 
        /// Returns True/False depending on if HandlerType is found. 
        /// Puts out the Type of handler that handles the TRequest.
        /// </summary>
        /// <typeparam name="TRequest">The Type of Request object that a Handler is needed for.</typeparam>
        /// <param name="request">The Request Object that a handler is needed for.</param>
        /// <param name="handler">The Type of IHandler that handles the TRequest.</param>
        /// <returns>
        /// True if a Type of IHandler is found, which is then put out. False if no IHandler is found, in which case it puts out null.
        /// </returns>
        public bool TryGetHandlerType<TRequest>(TRequest request, out Type handler) where TRequest : class;
    }
}
