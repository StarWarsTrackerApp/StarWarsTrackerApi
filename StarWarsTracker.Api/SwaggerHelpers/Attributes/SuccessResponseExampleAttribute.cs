using Swashbuckle.AspNetCore.Filters;
using System.Net;

namespace StarWarsTracker.Api.SwaggerHelpers.Attributes
{
    public class SuccessResponseExampleAttribute : SwaggerResponseExampleAttribute
    {
        public SuccessResponseExampleAttribute(Type examplesProviderType) : base((int)HttpStatusCode.OK, examplesProviderType)
        {
        }
    }
}
