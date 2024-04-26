using StarWarsTracker.Domain.Constants.LogConfigs;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace StarWarsTracker.Domain.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class DoesNotExistException : CustomException
    {
        public DoesNotExistException(string nameOfObjectNotExisting, params(object? Value, string NameOfValue)[] valuesSearchedBy)
        {
            NameOfObjectNotExisting = nameOfObjectNotExisting;

            ValuesSearchedBy = valuesSearchedBy;
        }

        public readonly string NameOfObjectNotExisting;

        public readonly (object?, string)[] ValuesSearchedBy;

        public override int GetStatusCode() => (int)HttpStatusCode.NotFound;

        public override CustomExceptionResponse GetResponseBody() => new DoesNotExistResponse(NameOfObjectNotExisting, ValuesSearchedBy);

        public override string GetLogLevelConfigKey() => Key.DoesNotExistExceptionLogLevel;
    }
}
