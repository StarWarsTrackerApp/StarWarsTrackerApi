using StarWarsTracker.Domain.Logging;
using StarWarsTracker.Persistence.DataRequestObjects.EventRequests;

namespace StarWarsTracker.Application.Requests.EventRequests.GetByNameLike
{
    internal class GetEventsByNameLikeHandler : DataRequestResponseHandler<GetEventsByNameLikeRequest, GetEventsByNameLikeResponse>
    {
        public GetEventsByNameLikeHandler(IDataAccess dataAccess, ILogMessage logMessage) : base(dataAccess, logMessage) { }

        public override async Task<GetEventsByNameLikeResponse> GetResponseAsync(GetEventsByNameLikeRequest request)
        {
            _logMessage.AddInfo(this, "Getting Response For Request", request.GetType().Name);

            _logMessage.AddDebug(this, "Request Body", request);

            var events = await _dataAccess.FetchListAsync(new GetEventsByNameLike(request.Name));

            _logMessage.AddDebug(this, "Event DTO's Found", events);

            if (events.Any())
            {
                var response = new GetEventsByNameLikeResponse(events.Select(_ => _.AsDomainEvent()));

                _logMessage.AddInfo(this, "Response Found", response.GetType().Name);

                _logMessage.AddDebug(this, "Response Body", response);

                return response;
            }

            _logMessage.AddInfo(this, "No Events Found");

            throw new DoesNotExistException(nameof(Event), (request.Name, nameof(request.Name)));
        }
    }
}
