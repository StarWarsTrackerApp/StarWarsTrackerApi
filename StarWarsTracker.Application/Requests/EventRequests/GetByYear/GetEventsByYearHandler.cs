using StarWarsTracker.Logging.Abstraction;
using StarWarsTracker.Persistence.DataRequestObjects.EventRequests;

namespace StarWarsTracker.Application.Requests.EventRequests.GetByYear
{
    internal class GetEventsByYearHandler : DataRequestHandler<GetEventsByYearRequest>
    {
        public GetEventsByYearHandler(IDataAccess dataAccess, IClassLoggerFactory loggerFactory) : base(dataAccess, loggerFactory) { }

        internal protected override async Task<IResponse> HandleRequestAsync(GetEventsByYearRequest request)
        {
            var eventDtos = await _dataAccess.FetchListAsync(new GetEventsByYear(request.YearsSinceBattleOfYavin));

            var events = eventDtos.Any() ? eventDtos.Select(_ => _.AsDomainEvent()) : Enumerable.Empty<Event>();

            return Response.Success(events);
        }
    }
}
