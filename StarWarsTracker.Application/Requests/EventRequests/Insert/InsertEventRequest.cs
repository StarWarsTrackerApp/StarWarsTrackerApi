using StarWarsTracker.Domain.Validation.EnumValidation;
using StarWarsTracker.Domain.Validation.StringValidation;

namespace StarWarsTracker.Application.Requests.EventRequests.Insert
{
    public class InsertEventRequest :  IValidatable
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
            validator = new(
                new StringLengthLimitRule(Name, nameof(Name), MaxLength.EventName),
                new RequiredCanonTypeRule(CanonType, nameof(CanonType)),
                new StringRequiredRule(Description, nameof(Description))
            );

            return validator.IsPassingAllRules;
        }
    }
}
