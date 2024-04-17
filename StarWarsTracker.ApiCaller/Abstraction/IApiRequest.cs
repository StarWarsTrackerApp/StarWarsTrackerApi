using StarWarsTracker.ApiCaller.Implementation;
using System.Net.Http.Headers;

namespace StarWarsTracker.ApiCaller.Abstraction
{
    public interface IApiRequest
    {
        public HttpMethod GetHttpMethod();

        public string GetRoute();

        public object? GetRequestBody();

        public Func<HttpRequestHeaders> AddHeaders(HttpRequestHeaders headers);

        public Task<ApiResponse> ParseResponseAsync(HttpResponseMessage response);
    }
}
