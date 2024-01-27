namespace StarWarsTracker.Application.Requests.EventRequests.Delete
{
    public class DeleteEventByGuidRequest : RequiredEventGuidRequest, IRequest
    {
        public DeleteEventByGuidRequest() { }

        public DeleteEventByGuidRequest(Guid eventGuid) : base(eventGuid) { }
    }
}
