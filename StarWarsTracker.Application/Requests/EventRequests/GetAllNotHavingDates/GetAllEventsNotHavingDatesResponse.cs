namespace StarWarsTracker.Application.Requests.EventRequests.GetAllNotHavingDates
{
    public class GetAllEventsNotHavingDatesResponse
    {
        public GetAllEventsNotHavingDatesResponse() { }

        public GetAllEventsNotHavingDatesResponse(IEnumerable<Event> events) => Events = events;

        public IEnumerable<Event> Events { get; set; } = Enumerable.Empty<Event>();
    }
}
