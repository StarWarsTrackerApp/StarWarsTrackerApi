using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace StarWarsTracker.Api.SwaggerHelpers.Attributes
{
    [ExcludeFromCodeCoverage]
    public class SuccessResponseExampleAttribute : SwaggerResponseExampleAttribute
    {
        public SuccessResponseExampleAttribute(Type examplesProviderType) : base((int)HttpStatusCode.OK, examplesProviderType)
        {
        }
    }
}
