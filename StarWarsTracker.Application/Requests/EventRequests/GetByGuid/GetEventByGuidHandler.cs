using StarWarsTracker.Domain.Logging;
using StarWarsTracker.Persistence.DataRequestObjects.EventDateRequests;
using StarWarsTracker.Persistence.DataRequestObjects.EventRequests;

namespace StarWarsTracker.Application.Requests.EventRequests.GetByGuid
{
    internal class GetEventByGuidHandler : DataRequestResponseHandler<GetEventByGuidRequest, GetEventByGuidResponse>
    {
        public GetEventByGuidHandler(IDataAccess dataAccess, ILogMessage logMessage) : base(dataAccess, logMessage) { }

        public override async Task<GetEventByGuidResponse> GetResponseAsync(GetEventByGuidRequest request)
        {
            _logMessage.AddInfo(this, "Getting Response For Request", request.GetType().Name);

            _logMessage.AddDebug(this, "Request Body", request);

            var eventDTO = await _dataAccess.FetchAsync(new GetEventByGuid(request.EventGuid));

            _logMessage.AddDebug(this, "Event DTO", eventDTO);

            if (eventDTO == null)
            {
                _logMessage.AddInfo(this, "No Event Found With Guid", request.EventGuid);

                throw new DoesNotExistException(nameof(Event), (request.EventGuid, nameof(request.EventGuid)));
            }

            var response = new GetEventByGuidResponse { Event = eventDTO.AsDomainEvent() };

            _logMessage.AddTrace(this, "Fetching Event Dates");

            var eventDatesDTO = await _dataAccess.FetchListAsync(new GetEventDatesByEventId(eventDTO.Id));

            _logMessage.AddDebug(this, "EventDates DTO", eventDatesDTO);

            if (eventDatesDTO.Any())
            {
                var eventDates = eventDatesDTO.Select(_ => _.AsDomainEventDate());

                response.EventTimeFrame = new EventTimeFrame(eventDates);            
            }

            _logMessage.AddInfo(this, "Response Found", response.GetType().Name);

            _logMessage.AddDebug(this, "Response Body", response);

            return response;
        }
    }
}
