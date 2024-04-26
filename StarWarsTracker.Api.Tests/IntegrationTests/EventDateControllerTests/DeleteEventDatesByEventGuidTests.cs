using StarWarsTracker.Application.Requests.EventDateRequests.Delete;
using StarWarsTracker.Domain.Exceptions;
using StarWarsTracker.Domain.Models;
using StarWarsTracker.Domain.Validation;
using StarWarsTracker.Persistence.DataRequestObjects.EventRequests;
using StarWarsTracker.Tests.Shared.Helpers;

namespace StarWarsTracker.Api.Tests.IntegrationTests.EventDateControllerTests
{
    public class DeleteEventDatesByEventGuidTests : ControllerTest<EventDateController>
    {
        [Fact]
        public async Task DeleteEventDatesByEventGuid_Given_EventGuidNotProvided_ShouldThrow_ValidationFailureException_WithMessage_EventGuidRequired()
        {
            var request = new DeleteEventDatesByEventGuidRequest();

            var expectedMessage = ValidationFailureMessage.RequiredField(nameof(request.EventGuid));

            var exception = await Record.ExceptionAsync(async () => await _controller.DeleteEventDatesByEventGuid(request)) as ValidationFailureException;

            Assert.NotNull(exception);

            Assert.Equal(expectedMessage, exception.GetResponseBody().ValidationFailureReasons.Single());
        }

        [Fact]
        public async Task DeleteEventDatesByEventGuid_Given_EventNotExistingWithGuid_ShouldThrow_DoesNotExistException_WithEventBeingNameOfObjectNotExisting()
        {
            var expectedNameOfObjectNotExisting = nameof(Event);

            var request = new DeleteEventDatesByEventGuidRequest(Guid.NewGuid());

            var exception = await Record.ExceptionAsync(async () => await _controller.DeleteEventDatesByEventGuid(request)) as DoesNotExistException;

            Assert.NotNull(exception);

            Assert.Equal(expectedNameOfObjectNotExisting, exception.GetResponseBody().NameOfObjectNotExisting);
        }

        [Fact]
        public async Task DeleteEventDatesByEventGuid_Given_EventExistsButHasNoDates_ShouldThrow_DoesNotExistException_WithEventDateBeingNameOfObjectNotExisting()
        {
            var expectedNameOfObjectNotExisting = nameof(EventDate);

            var existingEvent = await TestEvent.InsertAndFetchEventAsync();

            var request = new DeleteEventDatesByEventGuidRequest(existingEvent.Guid);

            var exception = await Record.ExceptionAsync(async () => await _controller.DeleteEventDatesByEventGuid(request)) as DoesNotExistException;

            await TestDataAccess.SharedInstance.ExecuteAsync(new DeleteEventById(existingEvent.Id));

            Assert.NotNull(exception);

            Assert.Equal(expectedNameOfObjectNotExisting, exception.GetResponseBody().NameOfObjectNotExisting);
        }
    }
}
