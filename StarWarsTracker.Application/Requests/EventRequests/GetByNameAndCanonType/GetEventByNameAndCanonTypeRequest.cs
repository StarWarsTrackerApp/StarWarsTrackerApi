using StarWarsTracker.Domain.Validation.EnumValidation;
using StarWarsTracker.Domain.Validation.StringValidation;

namespace StarWarsTracker.Application.Requests.EventRequests.GetByNameAndCanonType
{
    public class GetEventByNameAndCanonTypeRequest : IRequestResponse<GetEventByNameAndCanonTypeResponse>, IValidatable
    {
        public GetEventByNameAndCanonTypeRequest() { }

        public GetEventByNameAndCanonTypeRequest(string name, CanonType canonType)
        {
            Name = name;
            CanonType = canonType;
        }

        public string Name { get; set; } = null!;

        public CanonType CanonType { get; set; }

        public bool IsValid(out Validator validator)
        {
            validator = new(
                new StringLengthLimitRule(Name, nameof(Name), MaxLength.EventName),
                new RequiredCanonTypeRule(CanonType, nameof(CanonType))                
            );
            
            return validator.IsPassingAllRules;
        }
    }
}
