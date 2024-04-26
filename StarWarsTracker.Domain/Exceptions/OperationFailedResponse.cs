using System.Diagnostics.CodeAnalysis;

namespace StarWarsTracker.Domain.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class OperationFailedResponse
    {
        public string Message { get; set; } = "Unexpected Error";
    }
}
