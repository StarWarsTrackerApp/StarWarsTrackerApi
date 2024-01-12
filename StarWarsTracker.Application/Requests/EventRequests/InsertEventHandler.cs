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
            //TODO: Refactor to have validation implemented and throw exception for bad requests
            await _dataAccess.ExecuteAsync(new InsertEvent(request.Name, request.Description, (int)request.CanonType));
        }
    }
}
