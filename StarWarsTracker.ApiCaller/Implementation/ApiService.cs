using StarWarsTracker.ApiCaller.Abstraction;

namespace StarWarsTracker.ApiCaller.Implementation
{
    internal class ApiService : IApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private readonly IHttpRequestBuilder _requestBuilder;

        public ApiService(IHttpClientFactory httpClientFactory, IHttpRequestBuilder requestBuilder)
        {
            _httpClientFactory = httpClientFactory;
         
            _requestBuilder = requestBuilder;
        }

        public async Task<HttpResponseMessage?> GetResponseAsync(string baseUrl, IApiRequest request)
        {
            using var client = _httpClientFactory.CreateClient();

            var httpRequest = _requestBuilder.New(baseUrl, request);

            var response = await client.SendAsync(httpRequest);

            return response;
        }
    }
}
