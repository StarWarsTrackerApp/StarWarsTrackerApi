using StarWarsTracker.Logging.Abstraction;
using StarWarsTracker.Persistence.DataRequestObjects.EventDateRequests;
using StarWarsTracker.Persistence.DataRequestObjects.EventRequests;

namespace StarWarsTracker.Application.Requests.EventDateRequests.Delete
{
    internal class DeleteEventDatesByEventGuidHandler : DataRequestHandler<DeleteEventDatesByEventGuidRequest>
    {
        public DeleteEventDatesByEventGuidHandler(IDataAccess dataAccess, IClassLoggerFactory loggerFactory) : base(dataAccess, loggerFactory) { }

        public override async Task ExecuteRequestAsync(DeleteEventDatesByEventGuidRequest request)
        {
            _logger.AddInfo("Executing Request", request.GetType().Name);

            _logger.AddDebug("Request Body", request);

            var eventToDeleteDatesFor = await _dataAccess.FetchAsync(new GetEventByGuid(request.EventGuid));

            _logger.AddDebug("Event To Delete Dates For", eventToDeleteDatesFor);

            if(eventToDeleteDatesFor == null)
            {
                _logger.AddInfo("No Event Found With Guid", request.EventGuid);

                throw new DoesNotExistException(nameof(Event), (request.EventGuid, nameof(request.EventGuid)));
            }

            _logger.AddTrace("Getting EventDates To Delete");

            var eventDates = await _dataAccess.FetchListAsync(new GetEventDatesByEventId(eventToDeleteDatesFor.Id));

            _logger.AddDebug("EventDates Found", eventDates);

            if (!eventDates.Any())
            {
                _logger.AddInfo("No EventDates Found For Event");

                throw new DoesNotExistException(nameof(EventDate), (request.EventGuid, nameof(request.EventGuid)));
            }

            _logger.AddTrace("Deleting EventDates");

            var rowsImpacted = await _dataAccess.ExecuteAsync(new DeleteEventDatesByEventId(eventToDeleteDatesFor.Id));

            _logger.AddInfo("Count Of EventDates Deleted", rowsImpacted);

            if (rowsImpacted != eventDates.Count())
            {
                _logger.IncreaseLevel(LogLevel.Critical, "Count Of Event Dates To Delete Not Matching Count Deleted", 
                    new { ExpectedRowsImpacted = eventDates.Count(), ActualRowsImpacted = rowsImpacted });

                throw new OperationFailedException();
            }
        }
    }
}
