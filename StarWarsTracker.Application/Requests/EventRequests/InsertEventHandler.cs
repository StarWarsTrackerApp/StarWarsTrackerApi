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
            var guid = Guid.NewGuid();

            //TODO: Check if EventName is existing already for Canon, Legends, or CanonAndLegends type, as well as if Guid is already existing.

            //TODO: Check response and throw exception if request not inserted based on failure reason.
            await _dataAccess.ExecuteAsync(new InsertEvent(guid, request.Name, request.Description, (int)request.CanonType));

            //foreach (var eventDate in request.EventDates)
            //{
            //    await _dataAccess.ExecuteAsync(new InsertEventDate())
            //}
        }
    }
}
