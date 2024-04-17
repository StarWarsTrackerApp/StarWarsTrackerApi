namespace StarWarsTracker.BlazorServer.Api
{
    public class ApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<HttpResponseMessage?> GetResponseAsync(HttpRequestMessage request)
        {
            using var client = _httpClientFactory.CreateClient();

            var response = await client.SendAsync(request);

            return response;
        }
    }
}
