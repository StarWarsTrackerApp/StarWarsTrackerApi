namespace StarWarsTracker.BlazorServer.Api
{
    public class HttpRequestBuilder
    {
        public HttpRequestMessage New(string baseUrl, ApiRequest request)
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
