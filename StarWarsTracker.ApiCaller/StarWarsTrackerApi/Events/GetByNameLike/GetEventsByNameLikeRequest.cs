using StarWarsTracker.ApiCaller.BaseRequests;
using StarWarsTracker.ApiCaller.BaseResponses;
using StarWarsTracker.ApiCaller.Extensions;
using StarWarsTracker.ApiCaller.Implementation;
using StarWarsTracker.Domain.Constants.Routes;

namespace StarWarsTracker.ApiCaller.StarWarsTrackerApi.Events.GetByNameLike
{
    public class GetEventsByNameLikeRequest : GetRequest
    {
        #region Constructor

        public GetEventsByNameLikeRequest(string name)
        {
            Name = name;
        }

        #endregion

        #region Public Properties / Request Body

        public string Name { get; set; } = string.Empty;

        #endregion

        #region Api Request Methods

        public override object? GetRequestBody() => this;

        public override string GetRoute() => EventRoute.GetByNameLike;

        public override async Task<ApiResponse> ParseResponseAsync(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                if (response.TryParseContent<GetEventsByNameLikeResponse>(out var events))
                {
                    return new GetResponse<GetEventsByNameLikeResponse>(events);
                }

                return new UnexpectedResponse(response.StatusCode, "Unable to parse response.");
            }

            return await response.ParseStarWarsTrackerBadResponseAsync();
        }

        #endregion
    }
}
