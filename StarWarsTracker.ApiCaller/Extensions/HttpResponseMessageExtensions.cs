using StarWarsTracker.ApiCaller.BaseResponses;
using StarWarsTracker.ApiCaller.Implementation;
using System.Net;
using System.Text.Json;

namespace StarWarsTracker.ApiCaller.Extensions
{
    public static class HttpResponseMessageExtensions
    {
        public static bool TryParseContent<T>(this HttpResponseMessage message, out T content)
        {
            try
            {
                var json = message.Content.ReadAsStringAsync().Result;

                var options = new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                };

                var obj = JsonSerializer.Deserialize<T>(json, options);

                if(obj is null)
                {
                    content = default!;
                    return false;
                }

                content = obj;
                return true;
            }
            catch (Exception)
            {
                content = default!;
                return false;
            }
        }

        public static async Task<ApiResponse> ParseStarWarsTrackerBadResponseAsync(this HttpResponseMessage message)
        {            
            if (message.IsSuccessStatusCode)
            {
                return null!;
            }

            var json = await message.Content.ReadAsStringAsync();

            if (message.StatusCode == HttpStatusCode.BadRequest)
            {
                var validationFailureReasons = JsonSerializer.Deserialize<IEnumerable<string>>(json);

                if(validationFailureReasons != null)
                {
                    return new ValidationFailureResponse(validationFailureReasons);                    
                }
            }

            if (message.StatusCode == HttpStatusCode.NotFound)
            {
                return new NotFoundResponse(json);
            }

            return new UnexpectedResponse((int)message.StatusCode, json);
        }
    }
}
