using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace StarWarsTracker.Application.BaseObjects.ExceptionResponses
{
    [ExcludeFromCodeCoverage]
    public class ValidationFailureResponse : IResponse
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

        #region Public IResponse Methods

        public object? GetBody() => this;

        public int GetStatusCode() => (int)HttpStatusCode.BadRequest;

        #endregion
    }
}
