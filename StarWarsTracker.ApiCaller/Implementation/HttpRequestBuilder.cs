using StarWarsTracker.ApiCaller.Abstraction;
using System.Net.Http.Json;

namespace StarWarsTracker.ApiCaller.Implementation
{
    internal class HttpRequestBuilder : IHttpRequestBuilder
    {
        public HttpRequestMessage New(string baseUrl, IApiRequest request)
        {
            var httpRequest = new HttpRequestMessage(request.GetHttpMethod(), baseUrl + request.GetRoute());

            var requestBody = request.GetRequestBody();

            if (requestBody != null)
            {
                httpRequest.Content = JsonContent.Create(requestBody);
            }

            request.AddHeaders(httpRequest.Headers);

            return httpRequest;
        }
    }
}
