namespace StarWarsTracker.Application.BaseObjects.BaseHandlers
{
    internal abstract class DataOrchestratorRequestResponseHandler<TRequest, TResponse> : DataRequestResponseHandler<TRequest, TResponse> where TRequest : IRequestResponse<TResponse>
    {
        protected readonly IOrchestrator _orchestrator;

        protected DataOrchestratorRequestResponseHandler(IDataAccess dataAccess, IOrchestrator orchestrator) : base(dataAccess)
        {
            _orchestrator = orchestrator;
        }
    }

}
