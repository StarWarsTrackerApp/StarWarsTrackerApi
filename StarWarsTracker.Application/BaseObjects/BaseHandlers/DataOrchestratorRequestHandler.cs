using StarWarsTracker.Domain.Logging;

namespace StarWarsTracker.Application.BaseObjects.BaseHandlers
{
    internal abstract class DataOrchestratorRequestHandler<TRequest> : DataRequestHandler<TRequest> where TRequest : IRequest
    {
        protected readonly IOrchestrator _orchestrator;

        protected DataOrchestratorRequestHandler(IDataAccess dataAccess, ILogMessage logMessage, IOrchestrator orchestrator) : base(dataAccess, logMessage)
        {
            _orchestrator = orchestrator;
        }
    }
}
