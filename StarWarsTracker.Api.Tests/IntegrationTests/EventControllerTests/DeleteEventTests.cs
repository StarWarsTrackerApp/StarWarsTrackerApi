using StarWarsTracker.Api.Tests.TestHelpers;
using StarWarsTracker.Application.BaseObjects.ExceptionResponses;
using StarWarsTracker.Application.Requests.EventRequests.Delete;
using StarWarsTracker.Domain.Validation;
using StarWarsTracker.Tests.Shared.Helpers;

namespace StarWarsTracker.Api.Tests.IntegrationTests.EventControllerTests
{
    public class DeleteEventTests : ControllerTest<EventController>
    {
        [Fact]
        public async Task DeleteEvent_Given_EmptyGuid_ShouldReturn_BadRequestResponse_WithMessage_EventGuidRequired()
        {
            var request = new DeleteEventByGuidRequest(Guid.Empty);

            var expectedMessage = ValidationFailureMessage.RequiredField(nameof(request.EventGuid));

            var result = await _controller.DeleteEvent(request);

            var body = result.GetResponseBody<ValidationFailureResponse>();

            Assert.NotNull(body);

            Assert.Equal(expectedMessage, body.ValidationFailureReasons.Single());

            Assert.Equal(StatusCodes.Status400BadRequest, result.GetStatusCode());
        }

        [Fact]
        public async Task DeleteEvent_Given_EventNotExistingWithGuid_ShouldReturn_NotFoundResponse()
        {
            var result = await _controller.DeleteEvent(new(Guid.NewGuid()));

            Assert.Equal(StatusCodes.Status404NotFound, result.GetStatusCode());
        }

        [Fact]
        public async Task DeleteDevent_Given_EventDeleted_ShouldReturn_SuccessResponse()
        {
            var existingEvent = await TestEvent.InsertAndFetchEventAsync();

            var fetchBeforeDeleting = await _controller.GetEventByGuid(new(existingEvent.Guid));

            var result = await _controller.DeleteEvent(new DeleteEventByGuidRequest(existingEvent.Guid));

            var fetchAfterDeleting = await _controller.GetEventByGuid(new(existingEvent.Guid));

            Assert.Equal(StatusCodes.Status200OK, fetchBeforeDeleting.GetStatusCode());

            Assert.Equal(StatusCodes.Status200OK, result.GetStatusCode());
         
            Assert.Equal(StatusCodes.Status404NotFound, fetchAfterDeleting.GetStatusCode());
        }

        [Fact]
        public async Task DeleteEvent_Given_NullRequest_ShouldReturn_BadRequestException()
        {
            var result = await _controller.DeleteEvent(null!);

            Assert.Equal(StatusCodes.Status400BadRequest, result.GetStatusCode());
        }
    }
}
