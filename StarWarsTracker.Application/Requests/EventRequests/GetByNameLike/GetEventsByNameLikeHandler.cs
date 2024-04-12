using StarWarsTracker.Logging.Abstraction;
using StarWarsTracker.Persistence.DataRequestObjects.EventRequests;

namespace StarWarsTracker.Application.Requests.EventRequests.GetByNameLike
{
    internal class GetEventsByNameLikeHandler : DataRequestResponseHandler<GetEventsByNameLikeRequest, GetEventsByNameLikeResponse>
    {
        public GetEventsByNameLikeHandler(IDataAccess dataAccess, IClassLoggerFactory loggerFactory) : base(dataAccess, loggerFactory) { }

        public override async Task<GetEventsByNameLikeResponse> GetResponseAsync(GetEventsByNameLikeRequest request)
        {
            _logger.AddDebug("Request Body", request);

            var events = await _dataAccess.FetchListAsync(new GetEventsByNameLike(request.Name));

            _logger.AddDebug("Event DTO's Found", events);

            if (events.Any())
            {
                var response = new GetEventsByNameLikeResponse(events.Select(_ => _.AsDomainEvent()));

                _logger.AddInfo("Response Found", response.GetType().Name);

                return response;
            }

            _logger.AddInfo("No Events Found");

            throw new DoesNotExistException(nameof(Event), (request.Name, nameof(request.Name)));
        }
    }
}
