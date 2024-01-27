using StarWarsTracker.Persistence.DataRequestObjects.EventDateRequests;
using StarWarsTracker.Persistence.DataRequestObjects.EventRequests;

namespace StarWarsTracker.Application.Requests.EventRequests.GetByGuid
{
    internal class GetEventByGuidHandler : DataRequestResponseHandler<GetEventByGuidRequest, GetEventByGuidResponse>
    {
        public GetEventByGuidHandler(IDataAccess dataAccess) : base(dataAccess) { }

        public override async Task<GetEventByGuidResponse> GetResponseAsync(GetEventByGuidRequest request)
        {
            var eventDTO = await _dataAccess.FetchAsync(new GetEventByGuid(request.EventGuid));

            if (eventDTO == null)
            {
                throw new DoesNotExistException(nameof(Event), (request.EventGuid, nameof(request.EventGuid)));
            }

            var response = new GetEventByGuidResponse { Event = eventDTO.AsDomainEvent() };

            var eventDatesDTO = await _dataAccess.FetchListAsync(new GetEventDatesByEventId(eventDTO.Id));

            if (eventDatesDTO.Any())
            {
                var eventDates = eventDatesDTO.Select(_ => _.AsDomainEventDate());

                response.EventTimeFrame = new EventTimeFrame(eventDates);
            }

            return response;
        }
    }
}
