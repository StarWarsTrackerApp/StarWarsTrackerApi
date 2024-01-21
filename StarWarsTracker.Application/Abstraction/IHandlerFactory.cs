namespace StarWarsTracker.Application.Abstraction
{
    internal interface IHandlerFactory
    {
        /// <summary>
        /// Returns a new instance of the IRequestHandler which handles the IRequest provided.
        /// </summary>
        public IRequestHandler<TRequest> NewRequestHandler<TRequest>(TRequest request) where TRequest : IRequest;

        /// <summary>
        /// Returns a new instance of the IRequestResponseHandler which handles the IRequest TResponse provided
        /// </summary>
        public IRequestResponseHandler<TRequest, TResponse> NewRequestResponseHandler<TRequest, TResponse>(TRequest request) where TRequest : IRequest<TResponse>;
    }
}
