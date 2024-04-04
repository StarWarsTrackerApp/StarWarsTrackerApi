using StarWarsTracker.Domain.Logging;

namespace StarWarsTracker.Application.BaseObjects.BaseHandlers
{
    /// <summary>
    /// This abstract base class can be inherited from by any Handler for an IRequest that will use IDataAccess and returns a response object.
    /// </summary>
    internal abstract class DataRequestResponseHandler<TRequest, TResponse> : BaseRequestResponseHandler<TRequest, TResponse> where TRequest : IRequestResponse<TResponse>
    {
        protected readonly IDataAccess _dataAccess;

        protected DataRequestResponseHandler(IDataAccess dataAccess, ILogMessage logMessage) : base(logMessage) => _dataAccess = dataAccess;        
    }
}
