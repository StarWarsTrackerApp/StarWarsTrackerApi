namespace StarWarsTracker.Application.Abstraction
{
    /// <summary>
    /// This interface defines the contract for the factory that will instantiate a Handler at runtime using the Type of Request that is being received.
    /// </summary>
    internal interface IHandlerFactory
    {
        /// <summary>
        /// Uses the TRequest provided to locate the IBaseHandler that handles the TRequest.
        /// </summary>
        /// <typeparam name="TRequest">The IRequest or IRequestResponse that will be used to locate an IBaseHandler.</typeparam>
        /// <param name="request">The IRequest or IRequestResponse that will be used to locate an IBaseHandler. </param>
        /// <returns>Returns the IBaseHandler that handles the TRequest or throws a NotFoundException if there is no Handler found.</returns>
        public IBaseHandler NewHandler<TRequest>(TRequest request);
    }
}
