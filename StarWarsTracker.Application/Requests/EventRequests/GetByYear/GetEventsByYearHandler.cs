using StarWarsTracker.Application.BaseObjects.BaseHandlers;
using StarWarsTracker.Persistence.DataRequestObjects.EventRequests;

namespace StarWarsTracker.Application.Requests.EventRequests.GetByYear
{
    internal class GetEventsByYearHandler : DataRequestResponseHandler<GetEventsByYearRequest, GetEventsByYearResponse>
    {
        public GetEventsByYearHandler(IDataAccess dataAccess) : base(dataAccess) { }

        public override async Task<GetEventsByYearResponse> GetResponseAsync(GetEventsByYearRequest request)
        {
            var events = await _dataAccess.FetchListAsync(new GetEventsByYear(request.YearsSinceBattleOfYavin));

            if (events.Any())
            {
                return new GetEventsByYearResponse(events.Select(_ => new Event(_.Guid, _.Name, _.Description, (CanonType)_.CanonTypeId)));
            }

            throw new DoesNotExistException(nameof(Event), (request.YearsSinceBattleOfYavin, nameof(request.YearsSinceBattleOfYavin)));
        }
    }
}
