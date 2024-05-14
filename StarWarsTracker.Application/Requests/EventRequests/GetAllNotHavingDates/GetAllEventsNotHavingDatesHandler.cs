using StarWarsTracker.Logging.Abstraction;
using StarWarsTracker.Persistence.DataRequestObjects.EventRequests;

namespace StarWarsTracker.Application.Requests.EventRequests.GetAllNotHavingDates
{
    internal class GetAllEventsNotHavingDatesHandler : DataRequestHandler<GetAllEventsNotHavingDatesRequest>
    {
        public GetAllEventsNotHavingDatesHandler(IDataAccess dataAccess, IClassLoggerFactory loggerFactory) : base(dataAccess, loggerFactory) { }

        internal protected override async Task<IResponse> HandleRequestAsync(GetAllEventsNotHavingDatesRequest request)
        {
            var eventDtos = await _dataAccess.FetchListAsync(new GetAllEventsNotHavingDates());

            var events = eventDtos.Any() ? eventDtos.Select(_ => _.AsDomainEvent()) : Enumerable.Empty<Event>();

            return Response.Success(events);
        }
    }
}
