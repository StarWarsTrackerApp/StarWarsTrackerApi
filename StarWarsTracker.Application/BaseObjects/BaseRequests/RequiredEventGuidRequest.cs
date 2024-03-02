using StarWarsTracker.Domain.Validation.GuidValidation;

namespace StarWarsTracker.Application.BaseObjects.BaseRequests
{
    public abstract class RequiredEventGuidRequest : IValidatable
    {
        public RequiredEventGuidRequest() { }

        public RequiredEventGuidRequest(Guid eventGuid)
        {
            EventGuid = eventGuid;
        }

        public Guid EventGuid { get; set; }

        public bool IsValid(out Validator validator)
        {
            validator = new(new GuidRequiredRule(EventGuid, nameof(EventGuid)));

            return validator.IsPassingAllRules;
        }
    }
}
