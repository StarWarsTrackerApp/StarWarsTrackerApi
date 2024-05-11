using StarWarsTracker.Api.SwaggerHelpers.ExampleProviders;
using StarWarsTracker.Domain.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;

namespace StarWarsTracker.Api.Examples.Events
{
    [ExcludeFromCodeCoverage]
    public class GetAllEventsNotHavingDatesExample
    {
        public class SuccessResponse : IManyExamples<IEnumerable<Event>>
        {
            public IEnumerable<SwaggerExample<IEnumerable<Event>>> GetExamples() =>
                new SwaggerExample<IEnumerable<Event>>[]
                {
                    SwaggerExample.Create("Events Found", ExampleModel.CollectionOfEvents),
                    SwaggerExample.Create("Events Not Found", Enumerable.Empty<Event>())
                };
        }
    }
}
