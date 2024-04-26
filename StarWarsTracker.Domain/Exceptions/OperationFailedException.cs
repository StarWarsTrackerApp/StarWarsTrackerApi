using StarWarsTracker.Domain.Constants.LogConfigs;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace StarWarsTracker.Domain.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class OperationFailedException : CustomException
    {
        public override string GetLogLevelConfigKey() => Key.DefaultExceptionLogLevel;

        public override CustomExceptionResponse GetResponseBody() => new OperationFailedResponse();

        public override int GetStatusCode() => (int)HttpStatusCode.InternalServerError;
    }
}
