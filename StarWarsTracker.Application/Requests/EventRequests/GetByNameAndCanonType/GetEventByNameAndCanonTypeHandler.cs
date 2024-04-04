using StarWarsTracker.Domain.Logging;
using StarWarsTracker.Persistence.DataRequestObjects.EventRequests;

namespace StarWarsTracker.Application.Requests.EventRequests.GetByNameAndCanonType
{
    internal class GetEventByNameAndCanonTypeHandler : DataRequestResponseHandler<GetEventByNameAndCanonTypeRequest, GetEventByNameAndCanonTypeResponse>
    {
        public GetEventByNameAndCanonTypeHandler(IDataAccess dataAccess, ILogMessage logMessage) : base(dataAccess, logMessage) { }

        public override async Task<GetEventByNameAndCanonTypeResponse> GetResponseAsync(GetEventByNameAndCanonTypeRequest request)
        {
            _logMessage.AddInfo(this, "Getting Response For Request", request.GetType().Name);

            _logMessage.AddDebug(this, "Request Body", request);

            var eventDTO = await _dataAccess.FetchAsync(new GetEventByNameAndCanonType(request.Name, (int)request.CanonType));

            _logMessage.AddDebug(this, "Event DTO", eventDTO);

            if (eventDTO == null)
            {
                _logMessage.AddInfo(this, "No Event Found");

                throw new DoesNotExistException(nameof(Event), (request.Name, nameof(request.Name)), (request.CanonType, nameof(request.CanonType)));
            }

            var response = new GetEventByNameAndCanonTypeResponse(eventDTO.AsDomainEvent());

            _logMessage.AddInfo(this, "Response Found", response.GetType().Name);

            _logMessage.AddDebug(this, "Response Body", response);

            return response;
        }
    }
}
