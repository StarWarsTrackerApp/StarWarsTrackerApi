using StarWarsTracker.Logging.Abstraction;

namespace StarWarsTracker.Application.BaseObjects.BaseHandlers
{
    /// <summary>
    /// This base class is used for Handlers that will return a response for an IRequestResponse and integrate with the DataAccess layer.
    /// </summary>
    /// <typeparam name="TRequest">The type of IRequestResponse to get a response for.</typeparam>
    /// <typeparam name="TResponse">The type of response that will be returned by the handler. This type is defined by the IRequestResponse</typeparam>
    internal abstract class DataRequestResponseHandler<TRequest, TResponse> : BaseRequestResponseHandler<TRequest, TResponse> where TRequest : IRequestResponse<TResponse>
    {
        protected readonly IDataAccess _dataAccess;

        protected DataRequestResponseHandler(IDataAccess dataAccess, IClassLoggerFactory loggerFactory) : base(loggerFactory) => _dataAccess = dataAccess;        
    }
}
