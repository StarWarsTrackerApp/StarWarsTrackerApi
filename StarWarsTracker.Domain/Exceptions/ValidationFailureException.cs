using StarWarsTracker.Application.BaseObjects.ExceptionResponses;
using StarWarsTracker.Domain.Constants.LogConfigs;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace StarWarsTracker.Domain.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class ValidationFailureException : CustomException
    {
        public ValidationFailureException(params string[] validationFailureMessages) 
        { 
            ValidationFailureMessages = validationFailureMessages;
        }

        public ValidationFailureException(IEnumerable<string> validationFailureMessages)
        {
            ValidationFailureMessages = validationFailureMessages;
        }

        public IEnumerable<string> ValidationFailureMessages { get; set; } = Enumerable.Empty<string>();

        public override string GetLogLevelConfigKey() => Key.ValidationFailureExceptionLogLevel;

        public override CustomExceptionResponse GetResponseBody() => new ValidationFailureResponse(ValidationFailureMessages);

        public override int GetStatusCode() => (int)HttpStatusCode.BadRequest;
    }
}
