using StarWarsTracker.Application.BaseObjects.BaseHandlers;
using StarWarsTracker.Persistence.DataRequestObjects.EventRequests;

namespace StarWarsTracker.Application.Requests.EventRequests.GetByNameLike
{
    internal class GetEventsByNameLikeHandler : DataRequestResponseHandler<GetEventsByNameLikeRequest, GetEventsByNameLikeResponse>
    {
        public GetEventsByNameLikeHandler(IDataAccess dataAccess) : base(dataAccess) { }

        public override async Task<GetEventsByNameLikeResponse> GetResponseAsync(GetEventsByNameLikeRequest request)
        {
            var events = await _dataAccess.FetchListAsync(new GetEventsByNameLike(request.Name));

            if (events.Any())
            {
                return new GetEventsByNameLikeResponse(events.Select(_ => new Event(_.Guid, _.Name, _.Description, (CanonType)_.CanonTypeId)));
            }

            throw new DoesNotExistException(nameof(Event), (request.Name, nameof(request.Name)));
        }
    }
}
