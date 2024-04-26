using System.Diagnostics.CodeAnalysis;

namespace StarWarsTracker.Application.BaseObjects.ExceptionResponses
{
    [ExcludeFromCodeCoverage]
    public class ValidationFailureResponse
    {
        #region Constructors

        public ValidationFailureResponse(IEnumerable<string> validationFailureReasons)
        {
            ValidationFailureReasons = validationFailureReasons;
        }

        public ValidationFailureResponse(params string[] validationFailureReasons)
        {
            ValidationFailureReasons = validationFailureReasons;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// The reasons that the request failed validation.
        /// </summary>
        public IEnumerable<string> ValidationFailureReasons { get; set; }

        #endregion
    }
}
