using StarWarsTracker.Api.SwaggerHelpers.ExampleProviders;
using StarWarsTracker.Application.Requests.EventRequests.GetAllNotHavingDates;
using StarWarsTracker.Domain.Enums;
using StarWarsTracker.Domain.Models;
using Swashbuckle.AspNetCore.Filters;

namespace StarWarsTracker.Api.Examples.Events
{
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
                            new Event(Guid.NewGuid(), "Name Of An Event Without A Date", "A mysterious Event that nobody knows when it happened.", CanonType.StrictlyCanon),
                            new Event(Guid.NewGuid(), "Yet Another Event Without A Date", "Not even Yoda knows when this happened.", CanonType.StrictlyLegends),
                        })),
                    SwaggerExample.Create("Events Not Found",
                        new GetAllEventsNotHavingDatesResponse())
                };
        }
    }
}
