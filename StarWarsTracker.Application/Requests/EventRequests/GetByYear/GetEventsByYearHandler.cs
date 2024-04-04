using StarWarsTracker.Domain.Logging;
using StarWarsTracker.Persistence.DataRequestObjects.EventRequests;

namespace StarWarsTracker.Application.Requests.EventRequests.GetByYear
{
    internal class GetEventsByYearHandler : DataRequestResponseHandler<GetEventsByYearRequest, GetEventsByYearResponse>
    {
        public GetEventsByYearHandler(IDataAccess dataAccess, ILogMessage logMessage) : base(dataAccess, logMessage) { }

        public override async Task<GetEventsByYearResponse> GetResponseAsync(GetEventsByYearRequest request)
        {
            _logMessage.AddInfo(this, "Getting Response For Request", request.GetType().Name);

            _logMessage.AddDebug(this, "Request Body", request);

            var events = await _dataAccess.FetchListAsync(new GetEventsByYear(request.YearsSinceBattleOfYavin));

            _logMessage.AddDebug(this, "Event DTOs Found", events);

            if (events.Any())
            {
                var response = new GetEventsByYearResponse(events.Select(_ => _.AsDomainEvent()));

                _logMessage.AddInfo(this, "Response Found", response.GetType().Name);

                _logMessage.AddDebug(this, "Response Body", response);

                return response;
            }

            _logMessage.AddInfo(this, "No Events Found");

            throw new DoesNotExistException(nameof(Event), (request.YearsSinceBattleOfYavin, nameof(request.YearsSinceBattleOfYavin)));
        }
    }
}
