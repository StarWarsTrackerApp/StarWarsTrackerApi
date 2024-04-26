using StarWarsTracker.Api.SwaggerHelpers.ExampleProviders;
using StarWarsTracker.Application.BaseObjects.ExceptionResponses;
using StarWarsTracker.Application.Requests.EventRequests.Delete;
using StarWarsTracker.Domain.Exceptions;
using StarWarsTracker.Domain.Models;
using StarWarsTracker.Domain.Validation;
using System.Diagnostics.CodeAnalysis;

namespace StarWarsTracker.Api.Examples.Events
{
    [ExcludeFromCodeCoverage]
    public class DeleteEventExample
    {
        public class BadRequest : DeleteEventByGuidRequest, IValidationFailureExample
        {
            public ValidationFailureResponse GetExamples() => new(ValidationFailureMessage.RequiredField(nameof(EventGuid)));
        }

        public class DoesNotExist : DeleteEventByGuidRequest, IDoesNotExistExample
        {
            public DoesNotExistResponse GetExamples() => new(nameof(Event), (Guid.NewGuid(), nameof(EventGuid)));
        }
    }
}
