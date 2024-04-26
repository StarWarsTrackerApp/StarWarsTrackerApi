using StarWarsTracker.Domain.Constants.LogConfigs;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace StarWarsTracker.Domain.Exceptions
{
    /// <summary>
    /// This Exception is used when an Operation fails for an unexpected/unknown reason.    
    /// StatusCode will use HttpStatusCode.InternalServerError (500)
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class OperationFailedException : CustomException
    {
        #region Public CustomException Methods

        public override string GetLogLevelConfigKey() => Key.DefaultExceptionLogLevel;

        public override OperationFailedResponse GetResponseBody() => new OperationFailedResponse();

        public override int GetStatusCode() => (int)HttpStatusCode.InternalServerError;
        
        #endregion
    }
}
