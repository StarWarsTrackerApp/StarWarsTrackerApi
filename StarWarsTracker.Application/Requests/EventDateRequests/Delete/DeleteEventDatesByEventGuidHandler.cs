using StarWarsTracker.Persistence.DataRequestObjects.EventDateRequests;
using StarWarsTracker.Persistence.DataRequestObjects.EventRequests;

namespace StarWarsTracker.Application.Requests.EventDateRequests.Delete
{
    internal class DeleteEventDatesByEventGuidHandler : DataRequestHandler<DeleteEventDatesByEventGuidRequest>
    {
        public DeleteEventDatesByEventGuidHandler(IDataAccess dataAccess) : base(dataAccess) { }

        public override async Task ExecuteRequestAsync(DeleteEventDatesByEventGuidRequest request)
        {
            var eventToDeleteDatesFor = await _dataAccess.FetchAsync(new GetEventByGuid(request.EventGuid));

            if(eventToDeleteDatesFor == null)
            {
                throw new DoesNotExistException(nameof(Event), (request.EventGuid, nameof(request.EventGuid)));
            }
            
            var eventDates = await _dataAccess.FetchListAsync(new GetEventDatesByEventId(eventToDeleteDatesFor.Id));

            if (!eventDates.Any())
            {
                throw new DoesNotExistException(nameof(EventDate), (request.EventGuid, nameof(request.EventGuid)));
            }

            var rowsImpacted = await _dataAccess.ExecuteAsync(new DeleteEventDatesByEventId(eventToDeleteDatesFor.Id));

            if (rowsImpacted != eventDates.Count())
            {
                throw new OperationFailedException();
            }
        }
    }
}
