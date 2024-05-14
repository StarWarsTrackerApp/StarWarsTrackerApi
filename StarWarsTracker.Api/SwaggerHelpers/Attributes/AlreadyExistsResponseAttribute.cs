using StarWarsTracker.Application.BaseObjects.ExceptionResponses;
using Swashbuckle.AspNetCore.Annotations;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace StarWarsTracker.Api.SwaggerHelpers.Attributes
{
    [ExcludeFromCodeCoverage]
    public class AlreadyExistsResponseAttribute : SwaggerResponseAttribute
    {
        public const string DefaultDescription = "Conflict Response";

        public AlreadyExistsResponseAttribute(string description = DefaultDescription) : base((int)HttpStatusCode.Conflict, description, typeof(AlreadyExistsResponse))
        {
        }
    }
}
