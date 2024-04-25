using StarWarsTracker.Domain.Exceptions;

namespace StarWarsTracker.Application.BaseObjects.ExceptionResponses
{
    public class ValidationFailureResponse : CustomExceptionResponse
    {
        public ValidationFailureResponse(IEnumerable<string> validationFailureReasons)
        {
            ValidationFailureReasons = validationFailureReasons;
        }

        public ValidationFailureResponse(params string[] validationFailureReasons)
        {
            ValidationFailureReasons = validationFailureReasons;
        }

        public IEnumerable<string> ValidationFailureReasons { get; set; }
    }
}
