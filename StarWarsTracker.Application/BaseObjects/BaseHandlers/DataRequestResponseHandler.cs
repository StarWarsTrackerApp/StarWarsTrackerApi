namespace StarWarsTracker.Application.BaseObjects.BaseHandlers
{
    /// <summary>
    /// This abstract base class can be inherited from by any Handler for an IRequest that will use IDataAccess and returns a response object.
    /// </summary>
    internal abstract class DataRequestResponseHandler<TRequest, TResponse> : IRequestResponseHandler<TRequest, TResponse> where TRequest : IRequestResponse<TResponse>
    {
        protected readonly IDataAccess _dataAccess;

        protected DataRequestResponseHandler(IDataAccess dataAccess) => _dataAccess = dataAccess;

        public abstract Task<TResponse> GetResponseAsync(TRequest request);
    }
}
