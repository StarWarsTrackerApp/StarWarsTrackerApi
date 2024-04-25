using StarWarsTracker.Api.SwaggerHelpers.ExampleProviders;
using StarWarsTracker.Application.BaseObjects.ExceptionResponses;
using StarWarsTracker.Application.Requests.EventRequests.GetByNameLike;
using StarWarsTracker.Domain.Enums;
using StarWarsTracker.Domain.Exceptions;
using StarWarsTracker.Domain.Models;
using StarWarsTracker.Domain.Validation;
using Swashbuckle.AspNetCore.Filters;

namespace StarWarsTracker.Api.Examples.Events
{
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
                new Event[]
                {
                new (Guid.NewGuid(), "Battle At Some Example Planet", "This Example Found due to having Battle in the Name", CanonType.StrictlyCanon),
                new (Guid.NewGuid(), "Battle At Some Other Example Planet", "This Battle is another example Event", CanonType.StrictlyLegends),
                new (Guid.NewGuid(), "Battle At Yet Another Example Planet", "This is yet another Example Event", CanonType.CanonAndLegends),
                });
        }
    }
}
