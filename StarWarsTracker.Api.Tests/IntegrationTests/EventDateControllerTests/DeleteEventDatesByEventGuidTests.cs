using StarWarsTracker.Api.Tests.TestHelpers;
using StarWarsTracker.Application.BaseObjects.ExceptionResponses;
using StarWarsTracker.Application.Requests.EventDateRequests.Delete;
using StarWarsTracker.Domain.Exceptions;
using StarWarsTracker.Domain.Models;
using StarWarsTracker.Domain.Validation;
using StarWarsTracker.Tests.Shared.Helpers;

namespace StarWarsTracker.Api.Tests.IntegrationTests.EventDateControllerTests
{
    public class DeleteEventDatesByEventGuidTests : ControllerTest<EventDateController>
    {
        [Fact]
        public async Task DeleteEventDatesByEventGuid_Given_EventGuidNotProvided_ShouldReturn_BadRequestResponse_WithMessage_EventGuidRequired()
        {
            var request = new DeleteEventDatesByEventGuidRequest();

            var expectedMessage = ValidationFailureMessage.RequiredField(nameof(request.EventGuid));

            var result = await _controller.DeleteEventDatesByEventGuid(request);

            var responseBody = result.GetResponseBody<ValidationFailureResponse>();

            Assert.Equal(StatusCodes.Status400BadRequest, result.GetStatusCode());

            Assert.NotNull(responseBody);

            Assert.Equal(expectedMessage, responseBody.ValidationFailureReasons.Single());
        }

        [Fact]
        public async Task DeleteEventDatesByEventGuid_Given_EventNotExistingWithGuid_ShouldReturn_NotFoundResponse_With_NameOfObjectNotExisting_Event()
        {
            var expectedNameOfObjectNotExisting = nameof(Event);

            var request = new DeleteEventDatesByEventGuidRequest(Guid.NewGuid());

            var result = await _controller.DeleteEventDatesByEventGuid(request);

            var responseBody = result.GetResponseBody<NotFoundResponse>();

            Assert.Equal(StatusCodes.Status404NotFound, result.GetStatusCode());

            Assert.NotNull(responseBody);

            Assert.Equal(expectedNameOfObjectNotExisting, responseBody.NameOfObjectNotExisting);
        }

        [Fact]
        public async Task DeleteEventDatesByEventGuid_Given_EventExistsButHasNoDates_ShouldReturn_NotFoundResponse_With_NameOfObjectNotExisting_EventDate()
        {
            var expectedNameOfObjectNotExisting = nameof(EventDate);

            var existingEvent = await TestEvent.InsertAndFetchEventAsync();

            var request = new DeleteEventDatesByEventGuidRequest(existingEvent.Guid);

            var result = await _controller.DeleteEventDatesByEventGuid(request);

            var responseBody = result.GetResponseBody<NotFoundResponse>();

            Assert.Equal(StatusCodes.Status404NotFound, result.GetStatusCode());

            Assert.NotNull(responseBody);

            Assert.Equal(expectedNameOfObjectNotExisting, responseBody.NameOfObjectNotExisting);
        }
    }
}
