using StarWarsTracker.Application.Requests.EventRequests.GetByGuid;
using StarWarsTracker.Domain.Logging;
using StarWarsTracker.Persistence.DataRequestObjects.EventDateRequests;

namespace StarWarsTracker.Application.Requests.EventDateRequests.Insert
{
    internal class InsertEventDatesHandler : DataOrchestratorRequestHandler<InsertEventDatesRequest>
    {
        public InsertEventDatesHandler(IDataAccess dataAccess, ILogMessage logMessage, IOrchestrator orchestrator) : base(dataAccess, logMessage, orchestrator) { }

        public override async Task ExecuteRequestAsync(InsertEventDatesRequest request)
        {
            _logMessage.AddInfo(this, "Executing Request", request.GetType().Name);

            _logMessage.AddDebug(this, "Request Body", request);

            var eventByGuid = await _orchestrator.GetRequestResponseAsync(new GetEventByGuidRequest(request.EventGuid));

            _logMessage.AddDebug(this, "Event To Add Dates To", eventByGuid);

            if(eventByGuid.EventTimeFrame != null)
            {
                _logMessage.AddInfo(this, "Event Already Contains Event Dates", request.EventGuid);

                throw new AlreadyExistsException(nameof(EventTimeFrame), (request.EventGuid, nameof(request.EventGuid)));
            }

            _logMessage.AddTrace(this, "Inserting Event Dates From Request");

            int rowsUpdated = 0;

            foreach (var date in request.EventDates)
            {
                _logMessage.AddTrace(this, "Inserting Event Date", date);

                rowsUpdated += await _dataAccess.ExecuteAsync(new InsertEventDate(request.EventGuid, (int)date.EventDateType, date.YearsSinceBattleOfYavin, date.Sequence));

                _logMessage.AddDebug(this, "Dates Inserted", rowsUpdated);
            }
            
            if (rowsUpdated != request.EventDates.Length)
            {
                _logMessage.IncreaseLevel(LogLevel.Critical, this, "Expected Number Of Dates Not Inserted",
                    new { ExpectedCountOfRowsUpdated = request.EventDates.Length, ActualCountOfRowsUpdated = rowsUpdated });

                throw new OperationFailedException();
            }
        }
    }
}
