using StarWarsTracker.Api.SwaggerHelpers.ExampleProviders;
using StarWarsTracker.Application.Requests.EventRequests.GetByYear;
using StarWarsTracker.Domain.Exceptions;
using StarWarsTracker.Domain.Models;
using System.Diagnostics.CodeAnalysis;

namespace StarWarsTracker.Api.Examples.Events
{
    [ExcludeFromCodeCoverage]
    public class GetEventsByYearExample
    {
        public class SuccessResponse : IExample<GetEventsByYearResponse>
        {
            public GetEventsByYearResponse GetExamples() =>
                new(new[]
                {
                    ExampleModel.Event,
                    ExampleModel.Event,
                    ExampleModel.Event,
                    ExampleModel.Event
                });
        }

        public class DoesNotExist : GetEventsByYearRequest, INotFoundExample
        {
            public NotFoundResponse GetExamples() => new(nameof(Event), (-19, nameof(YearsSinceBattleOfYavin)));
        }
    }
}
