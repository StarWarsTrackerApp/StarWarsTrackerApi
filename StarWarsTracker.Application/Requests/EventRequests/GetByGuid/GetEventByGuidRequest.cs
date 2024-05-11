namespace StarWarsTracker.Application.Requests.EventRequests.GetByGuid
{
    public class GetEventByGuidRequest : RequiredEventGuidRequest
    {
        public GetEventByGuidRequest() { }

        public GetEventByGuidRequest(Guid eventGuid) : base(eventGuid) { }
    }
}
