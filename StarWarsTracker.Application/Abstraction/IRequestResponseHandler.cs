namespace StarWarsTracker.Application.Abstraction
{
    /// <summary>
    /// This Handler is used for any class that will handle an IRequest that returns a Response.
    /// </summary>
    internal interface IRequestResponseHandler<TRequest, TResponse> : IBaseHandler where TRequest : IRequestResponse<TResponse>
    {
        public Task<TResponse> GetResponseAsync(TRequest request);
    }
}
