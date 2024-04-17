namespace StarWarsTracker.ApiCaller.Implementation
{
    public class StarWarsTrackerApiUrl
    {
        private readonly string _baseUrl;

        public StarWarsTrackerApiUrl(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        public string BaseUrl => _baseUrl;
    }
}
