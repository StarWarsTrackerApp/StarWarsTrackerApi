using StarWarsTracker.Application.Requests.EventRequests.GetByGuid;
using StarWarsTracker.Logging.Abstraction;
using StarWarsTracker.Persistence.DataRequestObjects.EventDateRequests;

namespace StarWarsTracker.Application.Requests.EventDateRequests.Insert
{
    internal class InsertEventDatesHandler : DataOrchestratorRequestHandler<InsertEventDatesRequest>
    {
        public InsertEventDatesHandler(IDataAccess dataAccess, IClassLoggerFactory loggerFactory, IOrchestrator orchestrator) : base(dataAccess, loggerFactory, orchestrator) { }

        public override async Task ExecuteRequestAsync(InsertEventDatesRequest request)
        {
            _logger.AddInfo("Executing Request", request.GetType().Name);

            _logger.AddDebug("Request Body", request);

            var eventByGuid = await _orchestrator.GetRequestResponseAsync(new GetEventByGuidRequest(request.EventGuid));

            _logger.AddDebug("Event To Add Dates To", eventByGuid);

            if(eventByGuid.EventTimeFrame != null)
            {
                _logger.AddInfo("Event Already Contains Event Dates", request.EventGuid);

                throw new AlreadyExistsException(nameof(EventTimeFrame), (request.EventGuid, nameof(request.EventGuid)));
            }

            _logger.AddTrace("Inserting Event Dates From Request");

            int rowsUpdated = 0;

            foreach (var date in request.EventDates)
            {
                _logger.AddTrace("Inserting Event Date", date);

                rowsUpdated += await _dataAccess.ExecuteAsync(new InsertEventDate(request.EventGuid, (int)date.EventDateType, date.YearsSinceBattleOfYavin, date.Sequence));

                _logger.AddDebug("Dates Inserted", rowsUpdated);
            }
            
            if (rowsUpdated != request.EventDates.Length)
            {
                _logger.IncreaseLevel(LogLevel.Critical, "Expected Number Of Dates Not Inserted",
                    new { ExpectedCountOfRowsUpdated = request.EventDates.Length, ActualCountOfRowsUpdated = rowsUpdated });

                throw new OperationFailedException();
            }
        }
    }
}
