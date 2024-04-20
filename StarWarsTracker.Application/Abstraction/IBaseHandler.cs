namespace StarWarsTracker.Application.Abstraction
{
    /// <summary>
    /// This Interface defines a common method that is used to handle a request.
    /// The IBaseHandler is implemented by the IRequestHandler and IRequestResponseHandler
    /// </summary>
    internal interface IBaseHandler 
    { 
        /// <summary>
        /// Receive an object that is an IRequest or IRequestResponse and handle the request. Return the response or null if no response.
        /// </summary>
        /// <param name="request">The IRequest or IRequestResponse to handle.</param>
        /// <returns>The response from handling the request or null if no response.</returns>
        internal Task<object?> HandleAsync(object request);
    }
}
