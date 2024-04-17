namespace StarWarsTracker.ApiCaller.Abstraction
{
    internal interface IHttpRequestBuilder
    {
        public HttpRequestMessage New(string baseUrl, IApiRequest request);
    }
}
