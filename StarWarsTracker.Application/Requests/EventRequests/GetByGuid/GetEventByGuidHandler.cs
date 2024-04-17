using StarWarsTracker.Logging.Abstraction;
using StarWarsTracker.Persistence.DataRequestObjects.EventDateRequests;
using StarWarsTracker.Persistence.DataRequestObjects.EventRequests;

namespace StarWarsTracker.Application.Requests.EventRequests.GetByGuid
{
    internal class GetEventByGuidHandler : DataRequestResponseHandler<GetEventByGuidRequest, GetEventByGuidResponse>
    {
        public GetEventByGuidHandler(IDataAccess dataAccess, IClassLoggerFactory loggerFactory) : base(dataAccess, loggerFactory) { }

        public override async Task<GetEventByGuidResponse> GetResponseAsync(GetEventByGuidRequest request)
        {
            _logger.AddInfo("Getting Response For Request", request.GetType().Name);

            _logger.AddDebug("Request Body", request);

            var eventDTO = await _dataAccess.FetchAsync(new GetEventByGuid(request.EventGuid));

            _logger.AddDebug("Event DTO", eventDTO);

            if (eventDTO == null)
            {
                _logger.AddInfo("No Event Found With Guid", request.EventGuid);

                throw new DoesNotExistException(nameof(Event), (request.EventGuid, nameof(request.EventGuid)));
            }

            var response = new GetEventByGuidResponse { Event = eventDTO.AsDomainEvent() };

            _logger.AddTrace("Fetching Event Dates");

            var eventDatesDTO = await _dataAccess.FetchListAsync(new GetEventDatesByEventId(eventDTO.Id));

            _logger.AddDebug("EventDates DTO", eventDatesDTO);

            if (eventDatesDTO.Any())
            {
                var eventDates = eventDatesDTO.Select(_ => _.AsDomainEventDate());

                response.EventTimeFrame = new EventTimeFrame(eventDates);            
            }

            _logger.AddInfo("Response Found", response.GetType().Name);

            return response;
        }
    }
}
