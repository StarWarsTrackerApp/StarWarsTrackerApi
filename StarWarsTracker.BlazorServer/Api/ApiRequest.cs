using System.Net.Http.Headers;

namespace StarWarsTracker.BlazorServer.Api
{
    public abstract class ApiRequest
    {
        public abstract HttpMethod GetHttpMethod();

        public abstract string GetRoute();

        public virtual object? GetRequestBody() => null;

        public virtual Func<HttpRequestHeaders> AddHeaders(HttpRequestHeaders headers) => null!;
    }
}
