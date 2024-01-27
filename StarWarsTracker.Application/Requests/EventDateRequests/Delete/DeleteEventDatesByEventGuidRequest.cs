namespace StarWarsTracker.Application.Requests.EventDateRequests.Delete
{
    public class DeleteEventDatesByEventGuidRequest : RequiredEventGuidRequest, IRequest
    {
        public DeleteEventDatesByEventGuidRequest() { }

        public DeleteEventDatesByEventGuidRequest(Guid eventGuid) : base(eventGuid) { }
    }
}
