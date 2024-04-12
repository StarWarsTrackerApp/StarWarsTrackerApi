using StarWarsTracker.Logging.Abstraction;
using StarWarsTracker.Persistence.DataRequestObjects.EventDateRequests;
using StarWarsTracker.Persistence.DataRequestObjects.EventRequests;

namespace StarWarsTracker.Application.Requests.EventRequests.Delete
{
    internal class DeleteEventByGuidHandler : DataRequestHandler<DeleteEventByGuidRequest>
    {
        public DeleteEventByGuidHandler(IDataAccess dataAccess, IClassLoggerFactory loggerFactory) : base(dataAccess, loggerFactory) { }

        public override async Task ExecuteRequestAsync(DeleteEventByGuidRequest request)
        {
            var eventToDelete = await _dataAccess.FetchAsync(new GetEventByGuid(request.EventGuid));

            if (eventToDelete == null)
            {
                _logger.AddInfo("No Event Found With Guid", request.EventGuid);

                throw new DoesNotExistException(nameof(Event), (request.EventGuid, nameof(request.EventGuid)));
            }

            _logger.AddDebug("Deleting EventDates", eventToDelete);

            var eventDatesDeleted = await _dataAccess.ExecuteAsync(new DeleteEventDatesByEventId(eventToDelete.Id));

            _logger.AddInfo($"Count of EventDate records Deleted: {eventDatesDeleted}");

            _logger.AddTrace("Deleting Event");

            var rowsImpacted = await _dataAccess.ExecuteAsync(new DeleteEventById(eventToDelete.Id));

            _logger.AddInfo($"Count of Event records Deleted: {rowsImpacted}");

            if (rowsImpacted <= 0)
            {
                _logger.IncreaseLevel(LogLevel.Critical, "Failed to delete Event", eventToDelete);

                throw new OperationFailedException();
            }            
        }
    }
}
