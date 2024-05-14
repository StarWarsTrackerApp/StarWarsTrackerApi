using StarWarsTracker.Logging.Abstraction;
using StarWarsTracker.Persistence.DataRequestObjects.EventDateRequests;
using StarWarsTracker.Persistence.DataRequestObjects.EventRequests;

namespace StarWarsTracker.Application.Requests.EventRequests.GetByGuid
{
    internal class GetEventByGuidHandler : DataRequestHandler<GetEventByGuidRequest>
    {
        public GetEventByGuidHandler(IDataAccess dataAccess, IClassLoggerFactory loggerFactory) : base(dataAccess, loggerFactory) { }

        internal protected override async Task<IResponse> HandleRequestAsync(GetEventByGuidRequest request)
        {
            var eventDTO = await _dataAccess.FetchAsync(new GetEventByGuid(request.EventGuid));

            if (eventDTO == null)
            {
                return Response.NotFound(nameof(Event), (request.EventGuid, nameof(request.EventGuid)));
            }

            var response = new GetEventByGuidResponse { Event = eventDTO.AsDomainEvent() };

            _logger.AddTrace("Fetching Event Dates");

            var eventDatesDTO = await _dataAccess.FetchListAsync(new GetEventDatesByEventId(eventDTO.Id));

            if (eventDatesDTO.Any())
            {
                var eventDates = eventDatesDTO.Select(_ => _.AsDomainEventDate());

                response.EventTimeFrame = new EventTimeFrame(eventDates);
            }

            return Response.Success(response);
        }
    }
}
