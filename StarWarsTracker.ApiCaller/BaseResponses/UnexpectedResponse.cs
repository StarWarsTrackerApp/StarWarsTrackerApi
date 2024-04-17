using StarWarsTracker.ApiCaller.Implementation;
using System.Net;

namespace StarWarsTracker.ApiCaller.BaseResponses
{
    public class UnexpectedResponse : ApiResponse
    {
        public UnexpectedResponse(int statusCode, object? content = null) : base(statusCode, content)
        {
        }

        public UnexpectedResponse(HttpStatusCode statusCode, object? content = null) : base(statusCode, content)
        {
        }
    }
}
