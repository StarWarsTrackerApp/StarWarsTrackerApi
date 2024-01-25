using StarWarsTracker.Domain.Validation.EnumValidation;
using StarWarsTracker.Domain.Validation.StringValidation;

namespace StarWarsTracker.Application.Requests.EventRequests.Insert
{
    public class InsertEventRequest : IRequest, IValidatable
    {
        public InsertEventRequest() { }

        public InsertEventRequest(string name, string description, CanonType canonType)
        {
            Name = name;
            Description = description;
            CanonType = canonType;
        }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public CanonType CanonType { get; set; } = 0;

        public bool IsValid(out Validator validator)
        {
            validator = new();

            validator.ApplyRule(new StringLengthLimitRule(Name, nameof(Name), MaxLength.EventName));

            validator.ApplyRule(new StringRequiredRule(Description, nameof(Description)));

            validator.ApplyRule(new RequiredCanonTypeRule(CanonType, nameof(CanonType)));

            return validator.IsPassingAllRules;
        }
    }
}
