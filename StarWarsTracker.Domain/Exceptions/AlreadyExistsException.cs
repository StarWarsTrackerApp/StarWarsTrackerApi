using StarWarsTracker.Application.BaseObjects.ExceptionResponses;
using StarWarsTracker.Domain.Constants.LogConfigs;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace StarWarsTracker.Domain.Exceptions
{
    /// <summary>
    /// This Exception is used when an issue arises because an object already exists.
    /// StatusCode will use HttpStatusCode.Conflict (409)
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class AlreadyExistsException : CustomException
    {
        #region Private Members

        private readonly AlreadyExistsResponse _response;

        #endregion

        #region Constructor 

        /// <summary>
        /// Create a new AlreadyExistsException with the nameOfObjectAlreadyExisting and possibleConflicts provided.
        /// </summary>
        /// <param name="nameOfObjectAlreadyExisting">The name of the object already existing.</param>
        /// <param name="possibleConflicts">collection of Touples of (object Value, string NameOfField) representing possible conflicts leading to the AlreadyExistsException.</param>
        public AlreadyExistsException(string nameOfObjectAlreadyExisting, params (object Value, string NameOfField)[] possibleConflicts)
        {
            _response = new(nameOfObjectAlreadyExisting, possibleConflicts);
        }

        #endregion

        #region Public CustomException Methods

        public override int GetStatusCode() => (int)HttpStatusCode.Conflict;

        public override AlreadyExistsResponse GetResponseBody() => _response;

        public override string GetLogLevelConfigKey() => Key.AlreadyExistsExceptionLogLevel;

        #endregion
    }
}
