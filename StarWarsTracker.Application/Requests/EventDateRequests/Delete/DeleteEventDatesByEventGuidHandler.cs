using StarWarsTracker.Domain.Logging;
using StarWarsTracker.Persistence.DataRequestObjects.EventDateRequests;
using StarWarsTracker.Persistence.DataRequestObjects.EventRequests;

namespace StarWarsTracker.Application.Requests.EventDateRequests.Delete
{
    internal class DeleteEventDatesByEventGuidHandler : DataRequestHandler<DeleteEventDatesByEventGuidRequest>
    {
        public DeleteEventDatesByEventGuidHandler(IDataAccess dataAccess, ILogMessage logMessage) : base(dataAccess, logMessage) { }

        public override async Task ExecuteRequestAsync(DeleteEventDatesByEventGuidRequest request)
        {
            _logMessage.AddInfo(this, "Executing Request", request.GetType().Name);

            _logMessage.AddDebug(this, "Request Body", request);

            var eventToDeleteDatesFor = await _dataAccess.FetchAsync(new GetEventByGuid(request.EventGuid));

            _logMessage.AddDebug(this, "Event To Delete Dates For", eventToDeleteDatesFor);

            if(eventToDeleteDatesFor == null)
            {
                _logMessage.AddInfo(this, "No Event Found With Guid", request.EventGuid);

                throw new DoesNotExistException(nameof(Event), (request.EventGuid, nameof(request.EventGuid)));
            }

            _logMessage.AddTrace(this, "Getting EventDates To Delete");

            var eventDates = await _dataAccess.FetchListAsync(new GetEventDatesByEventId(eventToDeleteDatesFor.Id));

            _logMessage.AddDebug(this, "EventDates Found", eventDates);

            if (!eventDates.Any())
            {
                _logMessage.AddInfo(this, "No EventDates Found For Event");

                throw new DoesNotExistException(nameof(EventDate), (request.EventGuid, nameof(request.EventGuid)));
            }

            _logMessage.AddTrace(this, "Deleting EventDates");

            var rowsImpacted = await _dataAccess.ExecuteAsync(new DeleteEventDatesByEventId(eventToDeleteDatesFor.Id));

            _logMessage.AddInfo(this, "Count Of EventDates Deleted", rowsImpacted);

            if (rowsImpacted != eventDates.Count())
            {
                _logMessage.IncreaseLevel(LogLevel.Critical, this, "Count Of Event Dates To Delete Not Matching Count Deleted", 
                    new { ExpectedRowsImpacted = eventDates.Count(), ActualRowsImpacted = rowsImpacted });

                throw new OperationFailedException();
            }
        }
    }
}
