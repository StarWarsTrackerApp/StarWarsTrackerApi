using StarWarsTracker.Logging.Abstraction;

namespace StarWarsTracker.Application.BaseObjects.BaseHandlers
{
    /// <summary>
    /// This base class is used for Handlers that will Execute an IRequest and integrate with the DataAccess layer.
    /// </summary>
    /// <typeparam name="TRequest">The type of IRequest to Execute</typeparam>
    internal abstract class DataRequestHandler<TRequest> : BaseRequestHandler<TRequest> where TRequest : IRequest
    {
        protected readonly IDataAccess _dataAccess;

        protected DataRequestHandler(IDataAccess dataAccess, IClassLoggerFactory loggerFactory) : base(loggerFactory) => _dataAccess = dataAccess;
    }
}
