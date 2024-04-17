using StarWarsTracker.ApiCaller.Implementation;
using System.Net;

namespace StarWarsTracker.ApiCaller.BaseResponses
{
    public class GetResponse<T> : ApiResponse
    {
        private readonly T _value;

        public GetResponse(T content) : base(HttpStatusCode.OK, content)
        {
            _value = content;
        }

        public T Value => _value;
    }
}
