using StarWarsTracker.Persistence.DataRequestObjects.EventDateRequests;
using StarWarsTracker.Persistence.DataRequestObjects.EventRequests;

namespace StarWarsTracker.Application.Requests.EventRequests.Delete
{
    internal class DeleteEventByGuidHandler : DataRequestHandler<DeleteEventByGuidRequest>
    {
        public DeleteEventByGuidHandler(IDataAccess dataAccess) : base(dataAccess) { }

        public override async Task ExecuteRequestAsync(DeleteEventByGuidRequest request)
        {
            var eventToDelete = await _dataAccess.FetchAsync(new GetEventByGuid(request.EventGuid));

            if (eventToDelete == null)
            {
                throw new DoesNotExistException(nameof(Event), (request.EventGuid, nameof(request.EventGuid)));
            }

            await _dataAccess.ExecuteAsync(new DeleteEventDatesByEventId(eventToDelete.Id));

            var rowsImpacted = await _dataAccess.ExecuteAsync(new DeleteEventById(eventToDelete.Id));

            if (rowsImpacted <= 0)
            {
                throw new OperationFailedException();
            }
        }
    }
}
