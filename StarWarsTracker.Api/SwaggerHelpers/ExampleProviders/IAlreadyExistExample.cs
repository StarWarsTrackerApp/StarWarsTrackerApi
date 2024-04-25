using StarWarsTracker.Application.BaseObjects.ExceptionResponses;
using Swashbuckle.AspNetCore.Filters;

namespace StarWarsTracker.Api.SwaggerHelpers.ExampleProviders
{
    public interface IAlreadyExistExample : IExamplesProvider<AlreadyExistsResponse>
    {
    }
}
