using StarWarsTracker.Domain.Validation.GuidValidation;

namespace StarWarsTracker.Application.Requests.EventRequests.GetByGuid
{
    public class GetEventByGuidRequest : IRequestResponse<GetEventByGuidResponse>, IValidatable
    {
        public GetEventByGuidRequest() { }

        public GetEventByGuidRequest(Guid eventGuid)
        {
            EventGuid = eventGuid;
        }

        public Guid EventGuid { get; set; }

        public bool IsValid(out Validator validator)
        {
            validator = new();

            validator.ApplyRule(new GuidRequiredRule(EventGuid, nameof(EventGuid)));

            return validator.IsPassingAllRules;
        }
    }
}
