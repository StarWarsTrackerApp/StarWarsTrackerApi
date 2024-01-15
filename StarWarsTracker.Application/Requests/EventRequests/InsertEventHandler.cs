using StarWarsTracker.Application.Abstraction;
using StarWarsTracker.Persistence.Abstraction;
using StarWarsTracker.Persistence.DataRequestObjects.EventRequests;

namespace StarWarsTracker.Application.Requests.EventRequests
{
    internal class InsertEventHandler : IRequestHandler<InsertEventRequest>
    {
        private readonly IDataAccess _dataAccess;

        public InsertEventHandler(IDataAccess dataAccess) => _dataAccess = dataAccess;

        public async Task HandleRequestAsync(InsertEventRequest request) 
        {
            //TODO: Check response and throw exception if request not inserted based on failure reason.
            await _dataAccess.ExecuteAsync(new InsertEvent(request.Name, request.Description, (int)request.CanonType));
        }
    }
}
