namespace StarWarsTracker.Application.Abstraction
{
    /// <summary>
    /// This BaseHandler is used for fetching all Handlers via Reflection at startup to add to IHandlerDictionary
    /// </summary>
    internal interface IBaseHandler { }

    /// <summary>
    /// This Handler is used for any class that will handle an IRequest and not return any response type.
    /// </summary>
    internal interface IRequestHandler<TRequest> : IBaseHandler where TRequest : IRequest
    {
        public Task ExecuteRequestAsync(TRequest request);
    }

    /// <summary>
    /// This Handler is used for any class that will handle an IRequest that returns a Response.
    /// </summary>
    internal interface IRequestResponseHandler<TRequest, TResponse> : IBaseHandler where TRequest : IRequestResponse<TResponse>
    {
        public Task<TResponse> GetResponseAsync(TRequest request);
    }
}
