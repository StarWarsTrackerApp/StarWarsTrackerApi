using StarWarsTracker.Domain.Validation.GuidValidation;

namespace StarWarsTracker.Application.BaseObjects.BaseRequests
{
    /// <summary>
    /// This base class can be used by IRequest or IRequestResponse classes that will have an EventGuid property that is required.
    /// </summary>
    public abstract class RequiredEventGuidRequest : IValidatable
    {
        #region Constructors

        public RequiredEventGuidRequest() { }

        public RequiredEventGuidRequest(Guid eventGuid)
        {
            EventGuid = eventGuid;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// EventGuid Required For Request
        /// </summary>        
        /// <example>53F5A23C-968E-4C0E-A195-79B55CD8068E</example>
        public Guid EventGuid { get; set; } = Guid.Empty;

        #endregion

        #region Public IValidatable Methods

        public bool IsValid(out Validator validator)
        {
            validator = new(new GuidRequiredRule(EventGuid, nameof(EventGuid)));

            return validator.IsPassingAllRules;
        }

        #endregion
    }
}
