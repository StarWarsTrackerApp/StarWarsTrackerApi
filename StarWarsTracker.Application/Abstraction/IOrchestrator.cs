namespace StarWarsTracker.Application.Abstraction
{
    /// <summary>
    /// This interface defines the contract for the Orchestrator that will act as a mediator between the request caller and the request handler.
    /// </summary>
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
