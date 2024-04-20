using StarWarsTracker.ApiCaller.Implementation;
using System.Net;

namespace StarWarsTracker.ApiCaller.BaseResponses
{
    public class NotFoundResponse : ApiResponse
    {
        public NotFoundResponse(object? content = null) : base(HttpStatusCode.NotFound, content)
        {
        }
    }
}
