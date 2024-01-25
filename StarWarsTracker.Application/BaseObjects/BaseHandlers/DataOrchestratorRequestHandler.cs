namespace StarWarsTracker.Application.BaseObjects.BaseHandlers
{
    internal abstract class DataOrchestratorRequestHandler<TRequest> : DataRequestHandler<TRequest> where TRequest : IRequest
    {
        protected readonly IOrchestrator _orchestrator;

        protected DataOrchestratorRequestHandler(IDataAccess dataAccess, IOrchestrator orchestrator) : base(dataAccess)
        {
            _orchestrator = orchestrator;
        }
    }
}
