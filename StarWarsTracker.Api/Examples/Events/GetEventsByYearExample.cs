using StarWarsTracker.Api.SwaggerHelpers.ExampleProviders;
using StarWarsTracker.Application.Requests.EventRequests.GetByYear;
using StarWarsTracker.Domain.Enums;
using StarWarsTracker.Domain.Exceptions;
using StarWarsTracker.Domain.Models;

namespace StarWarsTracker.Api.Examples.Events
{
    public class GetEventsByYearExample
    {
        public class SuccessResponse : IExample<GetEventsByYearResponse>
        {
            public GetEventsByYearResponse GetExamples() =>
                new(new Event[]
                {
                    new(Guid.NewGuid(), "The Death Of Count Dooku", "Count Dooku Died at the hands of Anakin Skywalker", CanonType.CanonAndLegends),
                    new(Guid.NewGuid(), "The Death Of General Grievous", "General Grievous was brought to an end by Obi Wan Kenobi", CanonType.CanonAndLegends)
                });
        }

        public class DoesNotExist : GetEventsByYearRequest, IDoesNotExistExample
        {
            public DoesNotExistResponse GetExamples() => new(nameof(Event), (-19, nameof(YearsSinceBattleOfYavin)));
        }
    }
}
