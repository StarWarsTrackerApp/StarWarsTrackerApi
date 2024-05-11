using StarWarsTracker.Logging.Abstraction;
using StarWarsTracker.Persistence.DataRequestObjects.EventRequests;

namespace StarWarsTracker.Application.Requests.EventRequests.GetByNameLike
{
    internal class GetEventsByNameLikeHandler : DataRequestHandler<GetEventsByNameLikeRequest>
    {
        public GetEventsByNameLikeHandler(IDataAccess dataAccess, IClassLoggerFactory loggerFactory) : base(dataAccess, loggerFactory) { }

        internal protected override async Task<IResponse> HandleRequestAsync(GetEventsByNameLikeRequest request)
        {
            var eventDtos = await _dataAccess.FetchListAsync(new GetEventsByNameLike(request.Name));

            var events = eventDtos.Any() ? eventDtos.Select(_ => _.AsDomainEvent()) : Enumerable.Empty<Event>();

            return Response.Success(events);
        }
    }
}
