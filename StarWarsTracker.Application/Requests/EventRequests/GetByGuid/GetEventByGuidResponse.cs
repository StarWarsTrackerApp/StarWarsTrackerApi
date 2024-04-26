namespace StarWarsTracker.Application.Requests.EventRequests.GetByGuid
{
    public class GetEventByGuidResponse
    {
        public GetEventByGuidResponse()
        {
            
        }

        public GetEventByGuidResponse(Event starWarsEvent, EventTimeFrame eventTimeFrame)
        {
            Event = starWarsEvent;
            EventTimeFrame = eventTimeFrame;
        }

        public Event Event { get; set; } = null!;

        public EventTimeFrame EventTimeFrame { get; set; } = null!;
    }
}
