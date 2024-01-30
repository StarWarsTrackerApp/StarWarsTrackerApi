using StarWarsTracker.Persistence.DataRequestObjects.EventRequests;

namespace StarWarsTracker.Application.Requests.EventRequests.GetByNameAndCanonType
{
    internal class GetEventByNameAndCanonTypeHandler : DataRequestResponseHandler<GetEventByNameAndCanonTypeRequest, GetEventByNameAndCanonTypeResponse>
    {
        public GetEventByNameAndCanonTypeHandler(IDataAccess dataAccess) : base(dataAccess) { }

        public override async Task<GetEventByNameAndCanonTypeResponse> GetResponseAsync(GetEventByNameAndCanonTypeRequest request)
        {
            var eventDTO = await _dataAccess.FetchAsync(new GetEventByNameAndCanonType(request.Name, (int)request.CanonType));

            if (eventDTO == null)
            {
                throw new DoesNotExistException(nameof(Event), (request.Name, nameof(request.Name)), (request.CanonType, nameof(request.CanonType)));
            }

            return new GetEventByNameAndCanonTypeResponse(eventDTO.AsDomainEvent());
        }
    }
}
