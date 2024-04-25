using Swashbuckle.AspNetCore.Filters;
using System.Net;

namespace StarWarsTracker.Api.SwaggerHelpers.Attributes
{
    public class AlreadyExistsExampleAttribute : SwaggerResponseExampleAttribute
    {
        public AlreadyExistsExampleAttribute(Type examplesProviderType) : base((int)HttpStatusCode.Conflict, examplesProviderType)
        {
        }
    }
}
