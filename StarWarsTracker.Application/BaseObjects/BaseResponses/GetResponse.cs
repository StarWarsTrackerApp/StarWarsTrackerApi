namespace StarWarsTracker.Application.BaseObjects.BaseResponses
{
    public class GetResponse<T> : IResponse
    {
        private readonly int _statusCode;

        private readonly T _content;
        
        public GetResponse(int statusCode, T content)
        {
            _content = content;

            _statusCode = statusCode;
        }

        public T Content => _content;

        public object? GetBody() => _content;

        public int GetStatusCode() => _statusCode;
    }
}
