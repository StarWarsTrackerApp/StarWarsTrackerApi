using StarWarsTracker.ApiCaller.Abstraction;
using StarWarsTracker.ApiCaller.BaseResponses;
using System.Net;

namespace StarWarsTracker.ApiCaller.Implementation
{
    public class StarWarsTrackerApiCaller : IApiCaller
    {
        private readonly IApiService _apiService;

        private readonly string _baseUrl;

        public StarWarsTrackerApiCaller(IApiService apiService, StarWarsTrackerApiUrl starWarsTrackerApiUrl)
        { 
            _apiService = apiService;

            _baseUrl = starWarsTrackerApiUrl.BaseUrl;
        }

        public async Task<ApiResponse> GetResponseAsync(IApiRequest request)
        {
            var httpResponseMessage = await _apiService.GetResponseAsync(_baseUrl, request);

            if(httpResponseMessage != null)
            {
                var response = await request.ParseResponseAsync(httpResponseMessage);

                return response;
            }

            return new UnexpectedResponse(HttpStatusCode.InternalServerError, $"Api Response Null - {_baseUrl}{request.GetRoute}");
        }
    }
}
