using Swashbuckle.AspNetCore.Filters;
using System.Net;

namespace StarWarsTracker.Api.SwaggerHelpers.Attributes
{
    public class DoesNotExistExampleAttribute : SwaggerResponseExampleAttribute
    {
        public DoesNotExistExampleAttribute(Type examplesProviderType) : base((int)HttpStatusCode.NotFound, examplesProviderType)
        {
        }
    }
}
