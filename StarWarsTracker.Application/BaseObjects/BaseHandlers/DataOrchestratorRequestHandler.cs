using StarWarsTracker.Logging.Abstraction;

namespace StarWarsTracker.Application.BaseObjects.BaseHandlers
{
    internal abstract class DataOrchestratorRequestHandler<TRequest> : DataRequestHandler<TRequest> where TRequest : IRequest
    {
        protected readonly IOrchestrator _orchestrator;

        protected DataOrchestratorRequestHandler(IDataAccess dataAccess, IClassLoggerFactory loggerFactory, IOrchestrator orchestrator) : base(dataAccess, loggerFactory)
        {
            _orchestrator = orchestrator;
        }
    }
}
