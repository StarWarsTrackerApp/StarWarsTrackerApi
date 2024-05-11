using StarWarsTracker.Domain.Exceptions;
using Swashbuckle.AspNetCore.Annotations;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace StarWarsTracker.Api.SwaggerHelpers.Attributes
{
    [ExcludeFromCodeCoverage]
    public class DoesNotExistResponseAttribute : SwaggerResponseAttribute
    {
        public const string DefaultDescription = "Not Found Response";

        public DoesNotExistResponseAttribute(string description = DefaultDescription) : base((int)HttpStatusCode.NotFound, description, typeof(NotFoundResponse))
        {
        }
    }
}
