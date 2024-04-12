using StarWarsTracker.Logging.Abstraction;

namespace StarWarsTracker.Application.BaseObjects.BaseHandlers
{
    /// <summary>
    /// This abstract base class can be inherited from by any Handler for an IRequest that will use IDataAccess and does not return a response.
    /// </summary>
    internal abstract class DataRequestHandler<TRequest> : BaseRequestHandler<TRequest> where TRequest : IRequest
    {
        protected readonly IDataAccess _dataAccess;

        protected DataRequestHandler(IDataAccess dataAccess, IClassLoggerFactory loggerFactory) : base(loggerFactory) => _dataAccess = dataAccess;
    }
}
