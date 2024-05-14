using Microsoft.AspNetCore.Mvc;

namespace StarWarsTracker.Api.Tests.TestHelpers
{
    public static class IActionResultExtensions
    {
        public static int GetStatusCode(this IActionResult result) =>
              result is StatusCodeResult s ? s.StatusCode
            : result is ObjectResult o ? o.StatusCode.GetValueOrDefault()
            : throw new ApplicationException("Unexpected Result: " + result.GetType().Name);

        public static T? GetResponseBody<T>(this IActionResult result) =>
            result is ObjectResult o && o.Value is T expectedType ? expectedType : default;
    }
}
