namespace StarWarsTracker.Application.BaseObjects.BaseResponses
{
    public class ExecuteResponse : IResponse
    {
        private readonly int _statusCode;

        public ExecuteResponse(int statusCode) => _statusCode = statusCode;

        public object? GetBody() => null;

        public int GetStatusCode() => _statusCode;
    }
}
