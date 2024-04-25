using Swashbuckle.AspNetCore.Filters;

namespace StarWarsTracker.Api.SwaggerHelpers.ExampleProviders
{
    public interface IManyExamples<T> : IMultipleExamplesProvider<T>
    {
    }
}
