using StarWarsTracker.Logging.Abstraction;

namespace StarWarsTracker.Application.BaseObjects.BaseHandlers
{
    /// <summary>
    /// This base class is used for Handlers will integrate with the DataAccess layer.
    /// </summary>
    /// <typeparam name="TRequest">The type of IRequest to Handle</typeparam>
    internal abstract class DataRequestHandler<TRequest> : BaseHandler<TRequest>
    {
        protected readonly IDataAccess _dataAccess;

        protected DataRequestHandler(IDataAccess dataAccess, IClassLoggerFactory loggerFactory) : base(loggerFactory) => _dataAccess = dataAccess;
    }
}
