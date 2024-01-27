namespace StarWarsTracker.Application.Requests.EventRequests.GetByGuid
{
    public class GetEventByGuidResponse
    {
        public Event Event { get; set; } = null!;

        public EventTimeFrame EventTimeFrame { get; set; } = null!;
    }
}
