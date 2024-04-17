using System.Net;

namespace StarWarsTracker.ApiCaller.Implementation
{
    public abstract class ApiResponse
    {
        private readonly int _statusCode;

        private readonly object? _content;

        public ApiResponse(HttpStatusCode statusCode, object? content = null) : this((int)statusCode, content)
        {

        }

        public ApiResponse(int statusCode, object? content = null)
        {
            _statusCode = statusCode;

            _content = content;
        }

        public int StatusCode => _statusCode;

        public object? Content => _content;
    }
}
