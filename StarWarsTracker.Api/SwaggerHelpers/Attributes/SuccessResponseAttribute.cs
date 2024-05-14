using Swashbuckle.AspNetCore.Annotations;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace StarWarsTracker.Api.SwaggerHelpers.Attributes
{
    [ExcludeFromCodeCoverage]
    public class SuccessResponseAttribute : SwaggerResponseAttribute
    {
        public const string DefaultDescription = "Success Response";

        public SuccessResponseAttribute(string description = DefaultDescription) : base((int)HttpStatusCode.OK, description)
        {
        }
        public SuccessResponseAttribute(Type type, string description = DefaultDescription) : base((int)HttpStatusCode.OK, description, type)
        {
        }
    }
}
