namespace StarWarsTracker.Application.Requests.EventRequests.Delete
{
    public class DeleteEventByGuidRequest : RequiredEventGuidRequest
    {
        public DeleteEventByGuidRequest() { }

        public DeleteEventByGuidRequest(Guid eventGuid) : base(eventGuid) { }
    }
}
