using StarWarsTracker.Application.BaseObjects.ExceptionResponses;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace StarWarsTracker.Api.SwaggerHelpers.Attributes
{
    public class AlreadyExistsResponseAttribute : SwaggerResponseAttribute
    {
        public const string DefaultDescription = "Conflict Response";

        public AlreadyExistsResponseAttribute(string description = DefaultDescription) : base((int)HttpStatusCode.Conflict, description, typeof(AlreadyExistsResponse))
        {
        }
    }
}
