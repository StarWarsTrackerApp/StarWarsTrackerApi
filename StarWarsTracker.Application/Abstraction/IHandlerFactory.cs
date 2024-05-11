namespace StarWarsTracker.Application.Abstraction
{
    /// <summary>
    /// This interface defines the contract for the factory that will instantiate a Handler at runtime using the Type of Request that is being received.
    /// </summary>
    public interface IHandlerFactory
    {
        /// <summary>
        /// Instantiates and returns the type of IHandler that handles the TRequest provided.
        /// </summary>
        /// <typeparam name="TRequest">The type of request that an IHandler needs to be instantiated for.</typeparam>
        /// <param name="request">The request object that a handler needs to be instantiated for.</param>
        /// <returns>IHandler that handles the TRequest provided.</returns>
        public IHandler<TRequest> GetHandler<TRequest>(TRequest request) where TRequest : class;
    }
}
