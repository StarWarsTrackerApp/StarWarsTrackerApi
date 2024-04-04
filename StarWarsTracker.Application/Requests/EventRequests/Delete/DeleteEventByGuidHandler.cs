using StarWarsTracker.Domain.Logging;
using StarWarsTracker.Persistence.DataRequestObjects.EventDateRequests;
using StarWarsTracker.Persistence.DataRequestObjects.EventRequests;

namespace StarWarsTracker.Application.Requests.EventRequests.Delete
{
    internal class DeleteEventByGuidHandler : DataRequestHandler<DeleteEventByGuidRequest>
    {
        public DeleteEventByGuidHandler(IDataAccess dataAccess, ILogMessage logMessage) : base(dataAccess, logMessage) { }

        public override async Task ExecuteRequestAsync(DeleteEventByGuidRequest request)
        {
            var eventToDelete = await _dataAccess.FetchAsync(new GetEventByGuid(request.EventGuid));

            if (eventToDelete == null)
            {
                _logMessage.AddInfo(this, "No Event Found With Guid", request.EventGuid);

                throw new DoesNotExistException(nameof(Event), (request.EventGuid, nameof(request.EventGuid)));
            }

            _logMessage.AddDebug(this, "Deleting EventDates", eventToDelete);

            var eventDatesDeleted = await _dataAccess.ExecuteAsync(new DeleteEventDatesByEventId(eventToDelete.Id));

            _logMessage.AddInfo(this, $"Count of EventDate records Deleted: {eventDatesDeleted}");

            _logMessage.AddTrace(this, "Deleting Event");

            var rowsImpacted = await _dataAccess.ExecuteAsync(new DeleteEventById(eventToDelete.Id));

            _logMessage.AddInfo(this, $"Count of Event records Deleted: {rowsImpacted}");

            if (rowsImpacted <= 0)
            {
                _logMessage.IncreaseLevel(LogLevel.Critical, this, "Failed to delete Event", eventToDelete);

                throw new OperationFailedException();
            }            
        }
    }
}
