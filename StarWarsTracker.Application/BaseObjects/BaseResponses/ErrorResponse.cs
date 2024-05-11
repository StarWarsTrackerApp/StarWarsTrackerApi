using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace StarWarsTracker.Domain.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class ErrorResponse : IResponse
    {
        public ErrorResponse(string message) => Message = message;

        public string Message { get; set; }

        public object? GetBody() => this;

        public int GetStatusCode() => (int)HttpStatusCode.InternalServerError;
    }
}
