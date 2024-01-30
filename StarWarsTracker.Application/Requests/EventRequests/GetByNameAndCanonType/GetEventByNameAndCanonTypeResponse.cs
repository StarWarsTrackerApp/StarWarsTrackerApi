namespace StarWarsTracker.Application.Requests.EventRequests.GetByNameAndCanonType
{
    public class GetEventByNameAndCanonTypeResponse
    {
        public GetEventByNameAndCanonTypeResponse(Event e) => Event = e;

        public Event Event { get; set; }
    }
}