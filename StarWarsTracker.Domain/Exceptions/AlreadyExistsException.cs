using StarWarsTracker.Application.BaseObjects.ExceptionResponses;
using StarWarsTracker.Domain.Constants.LogConfigs;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace StarWarsTracker.Domain.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class AlreadyExistsException : CustomException
    {
        public AlreadyExistsException(string nameOfObjectAlreadyExisting, params (object Value, string NameOfField)[] conflicts)
        {
            NameOfObjectAlreadyExisting = nameOfObjectAlreadyExisting;

            Conflicts = conflicts;
        }

        public readonly string NameOfObjectAlreadyExisting;

        public (object, string)[] Conflicts { get; set; }

        public override int GetStatusCode() => (int)HttpStatusCode.Conflict;

        public override CustomExceptionResponse GetResponseBody() => new AlreadyExistsResponse(NameOfObjectAlreadyExisting, Conflicts);

        public override string GetLogLevelConfigKey() => Key.AlreadyExistsExceptionLogLevel;
    }
}
