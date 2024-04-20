namespace StarWarsTracker.Application.Abstraction
{
    /// <summary>
    /// This interface defines the contract for the Handler Dictionary which will use RequestTypes to locate and return the appropriate RequestHandler.
    /// </summary>
    internal interface IHandlerDictionary
    {
        /// <summary>
        /// This method takes in the requestType and returns the Type of IHandler that handles the requestType.
        /// </summary>
        /// <param name="requestType">The type of request to obtain a Handler for.</param>
        /// <returns>The Handler that handles the Request</returns>
        public Type? GetHandlerType(Type requestType);
    }
}
