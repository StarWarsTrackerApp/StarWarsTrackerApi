using StarWarsTracker.Domain.Exceptions;
using Swashbuckle.AspNetCore.Filters;

namespace StarWarsTracker.Api.SwaggerHelpers.ExampleProviders
{
    public interface IDoesNotExistExample : IExamplesProvider<DoesNotExistResponse>
    {
    }
}
