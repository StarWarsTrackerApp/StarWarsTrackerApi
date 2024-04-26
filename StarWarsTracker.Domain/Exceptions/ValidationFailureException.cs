using StarWarsTracker.Application.BaseObjects.ExceptionResponses;
using StarWarsTracker.Domain.Constants.LogConfigs;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace StarWarsTracker.Domain.Exceptions
{
    /// <summary>
    /// This Exception is used when a request fails validation.
    /// StatusCode will use HttpStatusCode.BadRequest (400)
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ValidationFailureException : CustomException
    {
        #region Private Members

        private readonly ValidationFailureResponse _response;

        #endregion

        #region Constructors

        public ValidationFailureException(params string[]  validationFailureMessages) => _response = new(validationFailureMessages);

        public ValidationFailureException(IEnumerable<string> validationFailureMessages) => _response = new(validationFailureMessages);

        #endregion

        #region Public CustomException Methods

        public override string GetLogLevelConfigKey() => Key.ValidationFailureExceptionLogLevel;

        public override ValidationFailureResponse GetResponseBody() => _response;

        public override int GetStatusCode() => (int)HttpStatusCode.BadRequest;
        
        #endregion
    }
}
