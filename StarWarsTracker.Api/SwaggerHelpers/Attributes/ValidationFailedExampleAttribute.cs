using Swashbuckle.AspNetCore.Filters;
using System.Net;

namespace StarWarsTracker.Api.SwaggerHelpers.Attributes
{
    public class ValidationFailedExampleAttribute : SwaggerResponseExampleAttribute
    {
        public ValidationFailedExampleAttribute(Type examplesProviderType) : base((int)HttpStatusCode.BadRequest, examplesProviderType)
        {
        }
    }
}
