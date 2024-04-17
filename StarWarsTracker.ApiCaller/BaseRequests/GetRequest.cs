using StarWarsTracker.ApiCaller.Abstraction;
using StarWarsTracker.ApiCaller.Implementation;
using System.Net.Http.Headers;

namespace StarWarsTracker.ApiCaller.BaseRequests
{
    public abstract class GetRequest : IApiRequest
    {
        public Func<HttpRequestHeaders> AddHeaders(HttpRequestHeaders headers) => null!;

        public HttpMethod GetHttpMethod() => HttpMethod.Get;

        public abstract object? GetRequestBody();

        public abstract string GetRoute();

        public abstract Task<ApiResponse> ParseResponseAsync(HttpResponseMessage response);
    }
}
