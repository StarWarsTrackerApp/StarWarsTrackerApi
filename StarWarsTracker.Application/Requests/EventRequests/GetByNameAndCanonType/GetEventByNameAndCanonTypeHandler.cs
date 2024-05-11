using StarWarsTracker.Logging.Abstraction;
using StarWarsTracker.Persistence.DataRequestObjects.EventRequests;

namespace StarWarsTracker.Application.Requests.EventRequests.GetByNameAndCanonType
{
    internal class GetEventByNameAndCanonTypeHandler : DataRequestHandler<GetEventByNameAndCanonTypeRequest>
    {
        public GetEventByNameAndCanonTypeHandler(IDataAccess dataAccess, IClassLoggerFactory loggerFactory) : base(dataAccess, loggerFactory) { }

        internal protected override async Task<IResponse> HandleRequestAsync(GetEventByNameAndCanonTypeRequest request)
        {
            var eventDTO = await _dataAccess.FetchAsync(new GetEventByNameAndCanonType(request.Name, (int)request.CanonType));

            return eventDTO != null ? Response.Success(eventDTO.AsDomainEvent()) 
                                    : Response.NotFound(nameof(Event), (request.Name, nameof(request.Name)), (request.CanonType, nameof(request.CanonType)));
        }
    }
}
