using StarWarsTracker.Api.SwaggerHelpers.ExampleProviders;
using StarWarsTracker.Application.BaseObjects.ExceptionResponses;
using StarWarsTracker.Application.Requests.EventRequests.GetByNameAndCanonType;
using StarWarsTracker.Domain.Enums;
using StarWarsTracker.Domain.Exceptions;
using StarWarsTracker.Domain.Models;
using StarWarsTracker.Domain.Validation;
using Swashbuckle.AspNetCore.Filters;

namespace StarWarsTracker.Api.Examples.Events
{
    public class GetEventByNameAndCanonTypeExample
    {
        public class SuccessResponse : IExample<GetEventByNameAndCanonTypeResponse>
        {
            public GetEventByNameAndCanonTypeResponse GetExamples() =>
                new(new Event(Guid.NewGuid(), "Name Of Event Found", "This Event Brought To You By Event Name And Canon Type Match", CanonType.StrictlyCanon));
        }

        public class BadRequest : GetEventByNameAndCanonTypeRequest, IManyValidationFailureExamples
        {
            public IEnumerable<SwaggerExample<ValidationFailureResponse>> GetExamples() =>
                new[]
                {
                     SwaggerExample.Create("No Fields Provided",
                        new ValidationFailureResponse(
                            ValidationFailureMessage.RequiredField(nameof(Name)), 
                            ValidationFailureMessage.InvalidValue(0, nameof(CanonType))
                        )),
                    SwaggerExample.Create("Name Not Provided",
                        new ValidationFailureResponse(
                            ValidationFailureMessage.RequiredField(nameof(Name))
                        )),
                    SwaggerExample.Create("CanonType Not Provided",
                        new ValidationFailureResponse(
                            ValidationFailureMessage.InvalidValue(0, nameof(CanonType))
                        ))
                };
        }

        public class DoesNotExist : GetEventByNameAndCanonTypeRequest, IDoesNotExistExample
        {
            public DoesNotExistResponse GetExamples() => new(nameof(Event), ("Name Of Event Searched For", nameof(Name)), (CanonType.StrictlyCanon, nameof(CanonType)));
        }
    }
}
