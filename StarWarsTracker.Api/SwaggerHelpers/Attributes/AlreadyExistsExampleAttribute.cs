using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace StarWarsTracker.Api.SwaggerHelpers.Attributes
{
    [ExcludeFromCodeCoverage]
    public class AlreadyExistsExampleAttribute : SwaggerResponseExampleAttribute
    {
        public AlreadyExistsExampleAttribute(Type examplesProviderType) : base((int)HttpStatusCode.Conflict, examplesProviderType)
        {
        }
    }
}
