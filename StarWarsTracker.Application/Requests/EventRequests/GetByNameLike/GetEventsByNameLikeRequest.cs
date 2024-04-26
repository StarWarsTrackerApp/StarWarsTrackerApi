using StarWarsTracker.Domain.Validation.StringValidation;

namespace StarWarsTracker.Application.Requests.EventRequests.GetByNameLike
{
    public class GetEventsByNameLikeRequest : IRequestResponse<GetEventsByNameLikeResponse>, IValidatable
    {
        #region Constructors

        public GetEventsByNameLikeRequest() { }

        public GetEventsByNameLikeRequest(string name) => Name = name;

        #endregion

        #region Public Property/Parameter

        /// <summary>
        /// The Name (or part of the Name) of the Event that you are searching for.
        /// </summary>
        /// <example>Battle</example>
        public string Name { get; set; } = string.Empty;

        #endregion

        #region Public IValidation Method

        public bool IsValid(out Validator validator)
        {
            validator = new(new StringLengthLimitRule(Name, nameof(Name), MaxLength.EventName));

            return validator.IsPassingAllRules;
        }

        #endregion
    }
}
