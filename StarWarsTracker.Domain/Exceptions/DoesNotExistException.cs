using StarWarsTracker.Domain.Constants.LogConfigs;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace StarWarsTracker.Domain.Exceptions
{
    /// <summary>
    /// This Exception is used when an object does not exist.
    /// StatusCode will use HttpStatusCode.NotFound (404)
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class DoesNotExistException : CustomException
    {
        #region Private Members

        private readonly DoesNotExistResponse _response;

        #endregion

        #region Constructor

        public DoesNotExistException(string nameOfObjectNotExisting, params(object? Value, string NameOfValue)[] valuesSearchedBy)
        {
            _response = new(nameOfObjectNotExisting, valuesSearchedBy);
        }

        #endregion

        #region Public CustomException Methods

        public override int GetStatusCode() => (int)HttpStatusCode.NotFound;

        public override DoesNotExistResponse GetResponseBody() => _response;

        public override string GetLogLevelConfigKey() => Key.DoesNotExistExceptionLogLevel;

        #endregion
    }
}
