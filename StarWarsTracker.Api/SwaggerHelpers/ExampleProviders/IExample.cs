using Swashbuckle.AspNetCore.Filters;

namespace StarWarsTracker.Api.SwaggerHelpers.ExampleProviders
{
    public interface IExample<T> : IExamplesProvider<T>
    {
    }
}
