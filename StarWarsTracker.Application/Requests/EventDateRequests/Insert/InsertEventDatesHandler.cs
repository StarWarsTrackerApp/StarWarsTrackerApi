using StarWarsTracker.Logging.Abstraction;
using StarWarsTracker.Persistence.DataRequestObjects.EventDateRequests;
using StarWarsTracker.Persistence.DataRequestObjects.EventRequests;

namespace StarWarsTracker.Application.Requests.EventDateRequests.Insert
{
    internal class InsertEventDatesHandler : DataRequestHandler<InsertEventDatesRequest>
    {
        public InsertEventDatesHandler(IDataAccess dataAccess, IClassLoggerFactory loggerFactory) : base(dataAccess, loggerFactory) { }

        internal protected override async Task<IResponse> HandleRequestAsync(InsertEventDatesRequest request)
        {
            var eventByGuid = await _dataAccess.FetchAsync(new GetEventByGuid(request.EventGuid));

            if (eventByGuid == null)
            {
                return Response.NotFound(nameof(Event), (request.EventGuid, nameof(request.EventGuid)));
            }

            var eventDates = await _dataAccess.FetchListAsync(new GetEventDatesByEventId(eventByGuid.Id));

            if (eventDates.Any())
            {
                return Response.AlreadyExists(nameof(EventTimeFrame), (request.EventGuid, nameof(request.EventGuid)));
            }

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

                return Response.Error();
            }

            return Response.Success();
        }
    }
}
