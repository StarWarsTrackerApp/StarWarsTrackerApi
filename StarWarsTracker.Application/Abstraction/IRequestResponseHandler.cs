namespace StarWarsTracker.Application.Abstraction
{
    /// <summary>
    /// This Handler is used for any class that will handle an IRequest that returns a Response.
    /// </summary>
    internal interface IRequestResponseHandler<TRequest, TResponse> : IBaseHandler where TRequest : IRequestResponse<TResponse>
    {
        /// <summary>
        /// Fetch/Handle the IRequestResponse received and return a TResponse that is defined by the IRequestResponse class.
        /// </summary>
        /// <param name="request">The IRequestResponse to be handled/fetch a response for</param>
        /// <returns>Response from handling the request. Is of type TResponse defined by the IRequestResponse handled.</returns>
        public Task<TResponse> GetResponseAsync(TRequest request);
    }
}
