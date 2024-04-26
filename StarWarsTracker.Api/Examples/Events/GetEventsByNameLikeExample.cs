using StarWarsTracker.Api.SwaggerHelpers.ExampleProviders;
using StarWarsTracker.Application.BaseObjects.ExceptionResponses;
using StarWarsTracker.Application.Requests.EventRequests.GetByNameLike;
using StarWarsTracker.Domain.Exceptions;
using StarWarsTracker.Domain.Models;
using StarWarsTracker.Domain.Validation;
using System.Diagnostics.CodeAnalysis;

namespace StarWarsTracker.Api.Examples.Events
{
    [ExcludeFromCodeCoverage]
    public class GetEventsByNameLikeExample
    {
        public class BadRequest : GetEventsByNameLikeRequest, IValidationFailureExample
        {
            public ValidationFailureResponse GetExamples() => new(ValidationFailureMessage.RequiredField(nameof(Name)));
        }

        public class DoesNotExist : GetEventsByNameLikeRequest, IDoesNotExistExample
        {
            public DoesNotExistResponse GetExamples() => new(nameof(Event), ("Name Searched By", nameof(Name)));
        }

        public class SuccessResponse : IExample<GetEventsByNameLikeResponse>
        {
            public GetEventsByNameLikeResponse GetExamples() => new(
                new[]
                {
                    ExampleModel.Event,
                    ExampleModel.Event,
                    ExampleModel.Event,
                    ExampleModel.Event
                });
        }
    }
}
