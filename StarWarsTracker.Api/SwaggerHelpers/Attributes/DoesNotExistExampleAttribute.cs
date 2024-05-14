using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace StarWarsTracker.Api.SwaggerHelpers.Attributes
{
    [ExcludeFromCodeCoverage]
    public class DoesNotExistExampleAttribute : SwaggerResponseExampleAttribute
    {
        public DoesNotExistExampleAttribute(Type examplesProviderType) : base((int)HttpStatusCode.NotFound, examplesProviderType)
        {
        }
    }
}
