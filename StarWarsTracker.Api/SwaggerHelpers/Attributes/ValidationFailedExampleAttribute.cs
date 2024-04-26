using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace StarWarsTracker.Api.SwaggerHelpers.Attributes
{
    [ExcludeFromCodeCoverage]
    public class ValidationFailedExampleAttribute : SwaggerResponseExampleAttribute
    {
        public ValidationFailedExampleAttribute(Type examplesProviderType) : base((int)HttpStatusCode.BadRequest, examplesProviderType)
        {
        }
    }
}
