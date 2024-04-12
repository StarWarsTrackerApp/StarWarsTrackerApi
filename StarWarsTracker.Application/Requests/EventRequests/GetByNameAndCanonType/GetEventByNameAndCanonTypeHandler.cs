using StarWarsTracker.Logging.Abstraction;
using StarWarsTracker.Persistence.DataRequestObjects.EventRequests;

namespace StarWarsTracker.Application.Requests.EventRequests.GetByNameAndCanonType
{
    internal class GetEventByNameAndCanonTypeHandler : DataRequestResponseHandler<GetEventByNameAndCanonTypeRequest, GetEventByNameAndCanonTypeResponse>
    {
        public GetEventByNameAndCanonTypeHandler(IDataAccess dataAccess, IClassLoggerFactory loggerFactory) : base(dataAccess, loggerFactory) { }

        public override async Task<GetEventByNameAndCanonTypeResponse> GetResponseAsync(GetEventByNameAndCanonTypeRequest request)
        {
            _logger.AddInfo("Getting Response For Request", request.GetType().Name);

            _logger.AddDebug("Request Body", request);

            var eventDTO = await _dataAccess.FetchAsync(new GetEventByNameAndCanonType(request.Name, (int)request.CanonType));

            _logger.AddDebug("Event DTO", eventDTO);

            if (eventDTO == null)
            {
                _logger.AddInfo("No Event Found");

                throw new DoesNotExistException(nameof(Event), (request.Name, nameof(request.Name)), (request.CanonType, nameof(request.CanonType)));
            }

            var response = new GetEventByNameAndCanonTypeResponse(eventDTO.AsDomainEvent());

            _logger.AddInfo("Response Found", response.GetType().Name);

            _logger.AddDebug("Response Body", response);

            return response;
        }
    }
}
