using StarWarsTracker.Application.BaseObjects.ExceptionResponses;
using StarWarsTracker.Domain.Exceptions;
using Swashbuckle.AspNetCore.Filters;

namespace StarWarsTracker.Api.SwaggerHelpers.ExampleProviders
{
    public interface INotFoundExample : IExamplesProvider<NotFoundResponse>
    {
    }
}
