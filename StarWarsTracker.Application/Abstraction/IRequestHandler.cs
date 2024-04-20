namespace StarWarsTracker.Application.Abstraction
{
    /// <summary>
    /// This Handler is used for any class that will handle an IRequest and not return any response type.
    /// </summary>
    internal interface IRequestHandler<TRequest> : IBaseHandler where TRequest : IRequest
    {
        /// <summary>
        /// Execute/Handle the IRequest received.
        /// </summary>
        /// <param name="request">The IRequest to be Executed</param>
        public Task ExecuteRequestAsync(TRequest request);
    }
}
