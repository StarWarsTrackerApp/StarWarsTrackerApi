using StarWarsTracker.ApiCaller.Implementation;

namespace StarWarsTracker.ApiCaller.Abstraction
{
    public interface IApiCaller 
    {
        public Task<ApiResponse> GetResponseAsync(IApiRequest request);
    }
}
