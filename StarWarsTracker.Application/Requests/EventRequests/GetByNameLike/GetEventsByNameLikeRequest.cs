using StarWarsTracker.Domain.Validation.StringValidation;

namespace StarWarsTracker.Application.Requests.EventRequests.GetByNameLike
{
    public class GetEventsByNameLikeRequest : IRequestResponse<GetEventsByNameLikeResponse>, IValidatable
    {
        public GetEventsByNameLikeRequest() { }

        public GetEventsByNameLikeRequest(string name) => Name = name;

        public string Name { get; set; } = null!;

        public bool IsValid(out Validator validator)
        {
            validator = new();

            validator.ApplyRule(new StringLengthLimitRule(Name, nameof(Name), MaxLength.EventName));

            return validator.IsPassingAllRules;
        }
    }
}
