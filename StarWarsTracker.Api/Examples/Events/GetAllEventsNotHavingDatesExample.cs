using StarWarsTracker.Api.SwaggerHelpers.ExampleProviders;
using StarWarsTracker.Application.Requests.EventRequests.GetAllNotHavingDates;
using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;

namespace StarWarsTracker.Api.Examples.Events
{
    [ExcludeFromCodeCoverage]
    public class GetAllEventsNotHavingDatesExample
    {
        public class SuccessResponse : IManyExamples<GetAllEventsNotHavingDatesResponse>
        {
            public IEnumerable<SwaggerExample<GetAllEventsNotHavingDatesResponse>> GetExamples() =>
                new[]
                {
                    SwaggerExample.Create("Events Found",
                        new GetAllEventsNotHavingDatesResponse(new[]
                        {
                            ExampleModel.Event,
                            ExampleModel.Event,
                            ExampleModel.Event,
                            ExampleModel.Event,
                        })),
                    SwaggerExample.Create("Events Not Found",
                        new GetAllEventsNotHavingDatesResponse())
                };
        }
    }
}
