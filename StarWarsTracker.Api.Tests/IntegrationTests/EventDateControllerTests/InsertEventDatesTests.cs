using StarWarsTracker.Application.Requests.EventDateRequests.Insert;
using StarWarsTracker.Domain.Exceptions;
using StarWarsTracker.Domain.Models;
using StarWarsTracker.Persistence.DataRequestObjects.EventDateRequests;
using StarWarsTracker.Persistence.DataRequestObjects.EventRequests;
using StarWarsTracker.Tests.Shared.Helpers;

namespace StarWarsTracker.Api.Tests.IntegrationTests.EventDateControllerTests
{
    public class InsertEventDatesTests : ControllerTest<EventDateController>
    {
        [Fact]
        public async Task InsertEventDate_Given_EventNotExistingWithGuid_ShouldThrow_DoesNotExistException_WithEventBeingNameOfObjectNotExisting()
        {
            var expectedNameOfObjectNotExisting = nameof(Event);

            var request = new InsertEventDatesRequest(Guid.NewGuid(), new EventDate[] { new(Domain.Enums.EventDateType.Definitive, 1, 0) });

            var exception = await Record.ExceptionAsync(async () => await _controller.InsertEventDates(request)) as DoesNotExistException;

            Assert.NotNull(exception);

            Assert.Equal(expectedNameOfObjectNotExisting, exception.NameOfObjectNotExisting);
        }

        [Fact]
        public async Task InsertEventDate_Given_EventExistsWithGuid_ButEventAlreadyHasDates_ShouldThrow_AlreadyExistsException_WithEventDateBeingNameOfObjectAlreadyExisting()
        {
            var expectedNameOfObjectAlreadyExisting = nameof(EventTimeFrame);

            var (eventAlreadyHavingEventDate, _) = await TestEventDate.InsertAndFetchEventDateAsync();

            var request = new InsertEventDatesRequest(eventAlreadyHavingEventDate.Guid, new EventDate[] { new(Domain.Enums.EventDateType.Definitive, 1, 0) });

            var exception = await Record.ExceptionAsync(async () => await _controller.InsertEventDates(request)) as AlreadyExistsException;

            await TestDataAccess.SharedInstance.ExecuteAsync(new DeleteEventDatesByEventId(eventAlreadyHavingEventDate.Id));
            await TestDataAccess.SharedInstance.ExecuteAsync(new DeleteEventById(eventAlreadyHavingEventDate.Id));

            Assert.NotNull(exception);

            Assert.Equal(expectedNameOfObjectAlreadyExisting, exception.NameOfObjectAlreadyExisting);
        }

        //TODO: Bad Request and Happy Path testing
    }
}
