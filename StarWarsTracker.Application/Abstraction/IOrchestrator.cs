namespace StarWarsTracker.Application.Abstraction
{
    public interface IOrchestrator
    {
        /// <summary>
        /// Executes the IRequest using the IRequestHandler associated with the request.
        /// </summary>
        public Task ExecuteRequestAsync<TRequest>(TRequest request) where TRequest : IRequest;

        /// <summary>
        /// Gets the Response for the IRequest TResponse using the IRequestResponseHandler associated with the request.
        /// </summary>
        public Task<TResponse> GetRequestResponseAsync<TResponse>(IRequestResponse<TResponse> request);
    }
}
