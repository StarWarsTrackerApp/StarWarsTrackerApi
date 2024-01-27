namespace StarWarsTracker.Application.Requests.EventRequests.GetByGuid
{
    public class GetEventByGuidRequest : RequiredEventGuidRequest, IRequestResponse<GetEventByGuidResponse>
    {
        public GetEventByGuidRequest() { }

        public GetEventByGuidRequest(Guid eventGuid) : base(eventGuid) { }
    }
}
