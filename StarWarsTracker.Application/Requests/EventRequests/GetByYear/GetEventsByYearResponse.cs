namespace StarWarsTracker.Application.Requests.EventRequests.GetByYear
{
    public class GetEventsByYearResponse
    {
        public GetEventsByYearResponse() { }

        public GetEventsByYearResponse(IEnumerable<Event> events)
        {
            Events = events;
        }

        public IEnumerable<Event> Events { get; set; } = Enumerable.Empty<Event>();
    }
}