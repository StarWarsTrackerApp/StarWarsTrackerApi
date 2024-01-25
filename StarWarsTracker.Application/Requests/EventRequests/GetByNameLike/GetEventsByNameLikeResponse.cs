namespace StarWarsTracker.Application.Requests.EventRequests.GetByNameLike
{
    public class GetEventsByNameLikeResponse
    {
        public GetEventsByNameLikeResponse() { }

        public GetEventsByNameLikeResponse(IEnumerable<Event> events) => Events = events;

        public IEnumerable<Event> Events { get; set; } = Enumerable.Empty<Event>();
    }
}