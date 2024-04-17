namespace StarWarsTracker.ApiCaller.Abstraction
{
    public interface IApiService
    {
        public Task<HttpResponseMessage?> GetResponseAsync(string baseUrl, IApiRequest request);
    }
}
