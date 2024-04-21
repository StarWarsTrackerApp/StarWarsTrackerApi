using StarWarsTracker.Logging.Abstraction;

namespace StarWarsTracker.Application.BaseObjects.BaseHandlers
{
    /// <summary>
    /// This base class is used for Handlers that will interact with the DataAccess layer as well as utilize other IRequest and/or IRequestResponses
    /// </summary>
    /// <typeparam name="TRequest">The type of IRequest to be executed</typeparam>
    internal abstract class DataOrchestratorRequestHandler<TRequest> : DataRequestHandler<TRequest> where TRequest : IRequest
    {
        protected readonly IOrchestrator _orchestrator;

        protected DataOrchestratorRequestHandler(IDataAccess dataAccess, IClassLoggerFactory loggerFactory, IOrchestrator orchestrator) : base(dataAccess, loggerFactory)
        {
            _orchestrator = orchestrator;
        }
    }
}
