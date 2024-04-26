using StarWarsTracker.Domain.Validation.EnumValidation;
using StarWarsTracker.Domain.Validation.StringValidation;

namespace StarWarsTracker.Application.Requests.EventRequests.GetByNameAndCanonType
{
    public class GetEventByNameAndCanonTypeRequest : IRequestResponse<GetEventByNameAndCanonTypeResponse>, IValidatable
    {
        #region Constructors

        public GetEventByNameAndCanonTypeRequest() { }

        public GetEventByNameAndCanonTypeRequest(string name, CanonType canonType)
        {
            Name = name;
            CanonType = canonType;
        }

        #endregion

        #region Public Properties / Query Parameters

        /// <summary>
        /// The name of the Event to search for.
        /// </summary>
        /// <example>Name To Search By</example>
        public string Name { get; set; } = null!;

        /// <summary>
        /// The CanonType of the Event to search for.
        /// Examples: Canon (1), Legends (2), or Canon And Legends (3)
        /// </summary>
        /// <example>1</example>
        public CanonType CanonType { get; set; }

        #endregion
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
