using StarWarsTracker.Api.SwaggerHelpers.ExampleProviders;
using StarWarsTracker.Application.BaseObjects.ExceptionResponses;
using StarWarsTracker.Application.Requests.EventRequests.GetByGuid;
using StarWarsTracker.Domain.Enums;
using StarWarsTracker.Domain.Models;
using StarWarsTracker.Domain.Validation;

namespace StarWarsTracker.Api.Examples.Events
{
    public class GetEventByGuidExample 
    {
        public class SuccessResponse : GetEventByGuidRequest, IExample<GetEventByGuidResponse>
        {
            public GetEventByGuidResponse GetExamples() =>
                new(
                   new Event(Guid.NewGuid(), "Name Of A Recently Identified Event", "This is a description of an Event", CanonType.StrictlyCanon),
                   new EventTimeFrame(new EventDate(EventDateType.Definitive, -10, 45))
                   );
        }

        public class BadRequest : GetEventByGuidRequest, IValidationFailureExample
        {
            public ValidationFailureResponse GetExamples() => new(ValidationFailureMessage.RequiredField(nameof(EventGuid)));
        }
    }
}
