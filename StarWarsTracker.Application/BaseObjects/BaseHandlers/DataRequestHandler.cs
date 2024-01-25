namespace StarWarsTracker.Application.BaseObjects.BaseHandlers
{
    /// <summary>
    /// This abstract base class can be inherited from by any Handler for an IRequest that will use IDataAccess and does not return a response.
    /// </summary>
    internal abstract class DataRequestHandler<TRequest> : IRequestHandler<TRequest> where TRequest : IRequest
    {
        protected readonly IDataAccess _dataAccess;

        protected DataRequestHandler(IDataAccess dataAccess) => _dataAccess = dataAccess;

        public abstract Task ExecuteRequestAsync(TRequest request);
    }
}
