namespace StarWarsTracker.Application.BaseObjects.BaseHandlers
{
    internal abstract class DataRequestHandler<TRequest> : IRequestHandler<TRequest> where TRequest : IRequest
    {
        protected readonly IDataAccess _dataAccess;

        protected DataRequestHandler(IDataAccess dataAccess) => _dataAccess = dataAccess;

        public abstract Task ExecuteRequestAsync(TRequest request);
    }

    internal abstract class DataRequestResponseHandler<TRequest, TResponse> : IRequestResponseHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        protected readonly IDataAccess _dataAccess;

        protected DataRequestResponseHandler(IDataAccess dataAccess) => _dataAccess = dataAccess;

        public abstract Task<TResponse> GetResponseAsync(TRequest request);
    }
}
