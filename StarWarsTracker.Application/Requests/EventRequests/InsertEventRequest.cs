using StarWarsTracker.Application.Abstraction;
using StarWarsTracker.Domain.Constants;
using StarWarsTracker.Domain.Enums;
using StarWarsTracker.Domain.Models;
using StarWarsTracker.Domain.Validation;
using StarWarsTracker.Domain.Validation.EnumValidation;
using StarWarsTracker.Domain.Validation.StringValidation;

namespace StarWarsTracker.Application.Requests.EventRequests
{
    public class InsertEventRequest : IRequest, IValidatable
    {
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public CanonType CanonType { get; set; } = 0;

        public IEnumerable<EventDate> EventDates { get; set; } = Enumerable.Empty<EventDate>();

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
