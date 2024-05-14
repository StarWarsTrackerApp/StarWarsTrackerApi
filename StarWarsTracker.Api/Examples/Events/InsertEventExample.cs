using StarWarsTracker.Api.SwaggerHelpers.ExampleProviders;
using StarWarsTracker.Application.BaseObjects.ExceptionResponses;
using StarWarsTracker.Application.Requests.EventRequests.Insert;
using StarWarsTracker.Domain.Enums;
using StarWarsTracker.Domain.Models;
using StarWarsTracker.Domain.Validation;
using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;

namespace StarWarsTracker.Api.Examples.Events
{
    [ExcludeFromCodeCoverage]
    public class InsertEventExample
    {
        public class ValidRequest : IExample<InsertEventRequest>
        {
            public InsertEventRequest GetExamples() =>
               new("Name Of New Event", "Description For New Event", CanonType.StrictlyCanon);
        }

        public class AlreadyExists : InsertEventRequest, IAlreadyExistExample
        {
            public AlreadyExistsResponse GetExamples() => new(
                nameof(Event),
                (CanonType.StrictlyCanon, nameof(CanonType)),
                ("Name Of Event", nameof(Name)));
        }

        public class BadRequest : InsertEventRequest, IManyValidationFailureExamples
        {
            public IEnumerable<SwaggerExample<ValidationFailureResponse>> GetExamples() =>
                new[]
                    {
                    SwaggerExample.Create("Missing All Required Fields",
                       new ValidationFailureResponse(
                           ValidationFailureMessage.RequiredField(nameof(Name)),
                           ValidationFailureMessage.RequiredField(nameof(Description)),
                           ValidationFailureMessage.InvalidValue(CanonType, nameof(CanonType))
                    )),
                    SwaggerExample.Create($"Missing Required Field: {nameof(Name)}",
                        new ValidationFailureResponse(
                            ValidationFailureMessage.RequiredField(nameof(Name))
                    )),
                    SwaggerExample.Create("Missing Required Field: Description",
                        new ValidationFailureResponse(
                            ValidationFailureMessage.RequiredField(nameof(Description))
                    )),
                    SwaggerExample.Create("CanonType Invalid Value",
                        "CanonType must be 1 (Canon), 2 (Legends), or 3 (Canon & Legends)",
                        new ValidationFailureResponse(
                            ValidationFailureMessage.InvalidValue(CanonType, nameof(CanonType))
                    ))
                };
        }
    }
}
