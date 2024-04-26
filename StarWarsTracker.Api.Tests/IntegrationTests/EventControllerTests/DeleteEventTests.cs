using StarWarsTracker.Application.Requests.EventRequests.Delete;
using StarWarsTracker.Domain.Exceptions;
using StarWarsTracker.Domain.Validation;
using StarWarsTracker.Tests.Shared.Helpers;

namespace StarWarsTracker.Api.Tests.IntegrationTests.EventControllerTests
{
    public class DeleteEventTests : ControllerTest<EventController>
    {
        [Fact]
        public async Task DeleteEvent_Given_EmptyGuid_ShouldThrow_ValidationFailureException_WithMessage_EventGuidIsRequired()
        {
            var request = new DeleteEventByGuidRequest(Guid.Empty);

            var expectedMessage = ValidationFailureMessage.RequiredField(nameof(request.EventGuid));

            var exception = await Record.ExceptionAsync(async () => await _controller.DeleteEvent(request)) as ValidationFailureException;

            Assert.NotNull(exception);

            Assert.Equal(expectedMessage, exception.ValidationFailureMessages.Single());
        }

        [Fact]
        public async Task DeleteEvent_Given_EventNotExistingWithEventGuid_ShouldThrow_DoesNotExistException()
        {
            await Assert.ThrowsAsync<DoesNotExistException>(async () => await _controller.DeleteEvent(new(Guid.NewGuid())));
        }

        [Fact]
        public async Task DeleteDevent_Given_TaskCompletedSuccesfully_Should_DeleteEvent()
        {
            var existingEvent = await TestEvent.InsertAndFetchEventAsync();

            var deleteTask = _controller.DeleteEvent(new DeleteEventByGuidRequest(existingEvent.Guid));

            await deleteTask;

            var doesNotExistExceptionAfterDeleting = await Record.ExceptionAsync(async () => await _controller.GetEventByGuid(new(existingEvent.Guid))) as DoesNotExistException;

            Assert.True(deleteTask.IsCompletedSuccessfully);

            // Assert that attempting to fetch the event after inserting throws a does not exist exception
            Assert.NotNull(doesNotExistExceptionAfterDeleting);
        }

        [Fact]
        public async Task DeleteEvent_Given_NullRequest_ShouldThrow_ValidationFailureException()
        {
            await Assert.ThrowsAsync<ValidationFailureException>(async () => await _controller.DeleteEvent(null!));
        }
    }
}
