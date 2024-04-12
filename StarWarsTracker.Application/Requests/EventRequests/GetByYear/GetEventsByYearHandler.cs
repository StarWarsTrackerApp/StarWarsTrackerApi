using StarWarsTracker.Logging.Abstraction;
using StarWarsTracker.Persistence.DataRequestObjects.EventRequests;

namespace StarWarsTracker.Application.Requests.EventRequests.GetByYear
{
    internal class GetEventsByYearHandler : DataRequestResponseHandler<GetEventsByYearRequest, GetEventsByYearResponse>
    {
        public GetEventsByYearHandler(IDataAccess dataAccess, IClassLoggerFactory loggerFactory) : base(dataAccess, loggerFactory) { }

        public override async Task<GetEventsByYearResponse> GetResponseAsync(GetEventsByYearRequest request)
        {
            _logger.AddInfo("Getting Response For Request", request.GetType().Name);

            _logger.AddDebug("Request Body", request);

            var events = await _dataAccess.FetchListAsync(new GetEventsByYear(request.YearsSinceBattleOfYavin));

            _logger.AddDebug("Event DTOs Found", events);

            if (events.Any())
            {
                var response = new GetEventsByYearResponse(events.Select(_ => _.AsDomainEvent()));

                _logger.AddInfo("Response Found", response.GetType().Name);

                _logger.AddDebug("Response Body", response);

                return response;
            }

            _logger.AddInfo("No Events Found");

            throw new DoesNotExistException(nameof(Event), (request.YearsSinceBattleOfYavin, nameof(request.YearsSinceBattleOfYavin)));
        }
    }
}
