using StarWarsTracker.Api.SwaggerHelpers.ExampleProviders;
using StarWarsTracker.Application.BaseObjects.ExceptionResponses;
using StarWarsTracker.Application.Requests.EventRequests.GetByGuid;
using StarWarsTracker.Domain.Exceptions;
using StarWarsTracker.Domain.Models;
using StarWarsTracker.Domain.Validation;
using System.Diagnostics.CodeAnalysis;

namespace StarWarsTracker.Api.Examples.Events
{
    [ExcludeFromCodeCoverage]
    public class GetEventByGuidExample 
    {
        public class SuccessResponse : GetEventByGuidRequest, IExample<GetEventByGuidResponse>
        {
            public GetEventByGuidResponse GetExamples() => new(ExampleModel.Event, ExampleModel.EventTimeFrame);
        }

        public class BadRequest : GetEventByGuidRequest, IValidationFailureExample
        {
            public ValidationFailureResponse GetExamples() => new(ValidationFailureMessage.RequiredField(nameof(EventGuid)));
        }

        public class DoesNotExist : GetEventByGuidRequest, IDoesNotExistExample
        {
            public DoesNotExistResponse GetExamples() => new(nameof(Event), (Guid.NewGuid(), nameof(EventGuid)));
        }
    }
}
