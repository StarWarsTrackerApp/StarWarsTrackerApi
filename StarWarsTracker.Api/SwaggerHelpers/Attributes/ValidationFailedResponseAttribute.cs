using StarWarsTracker.Application.BaseObjects.ExceptionResponses;
using Swashbuckle.AspNetCore.Annotations;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace StarWarsTracker.Api.SwaggerHelpers.Attributes
{
    [ExcludeFromCodeCoverage]
    public class ValidationFailedResponseAttribute : SwaggerResponseAttribute
    {
        public const string DefaultDescription = "Bad Request Response";

        public ValidationFailedResponseAttribute(string description = DefaultDescription) : base((int)HttpStatusCode.BadRequest, description, typeof(ValidationFailureResponse))
        {
        }
    }
}
