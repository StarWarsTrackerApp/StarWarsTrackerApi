using StarWarsTracker.Tests.Shared.Helpers;

namespace StarWarsTracker.Api.Tests.IntegrationTests.EventControllerTests
{
    public class GetAllEventsNotHavingDatesTests : ControllerTest<EventController>
    {
        [Fact]
        public async Task GetAllEventsNotHavingDates_Given_EventInsertedHasNoDates_ShouldReturn_ResponseContainingEventInsertedWithoutDates()
        {
            var existingEventWithoutDates = await TestEvent.InsertAndFetchEventAsync();

            var response = await _controller.GetAllEventsNotHavingDates(new());

            await _controller.DeleteEvent(new(existingEventWithoutDates.Guid));

            // try to get the event that was inserted from response using the Guid
            var eventReturned = response.Events.Single(_ => _.Guid == existingEventWithoutDates.Guid);

            //Assert that the eventReturned is not null
            Assert.NotNull(eventReturned);

            // Assert that the properties on the event match
            Assert.Equal(existingEventWithoutDates.Guid, eventReturned.Guid);
            Assert.Equal(existingEventWithoutDates.Name, eventReturned.Name);
            Assert.Equal(existingEventWithoutDates.Description, eventReturned.Description);
            Assert.Equal(existingEventWithoutDates.CanonTypeId, (int)eventReturned.CanonType);
        }

        [Fact]
        public async Task GetAllEventsNotHavingDates_Given_EventInsertedHasDates_ShouldReturn_ResponseNotContainingEventInsertedWithDates()
        {
            var (existingEventWithDates, _) = await TestEventDate.InsertAndFetchEventDateAsync();

            var response = await _controller.GetAllEventsNotHavingDates(new());

            await _controller.DeleteEvent(new(existingEventWithDates.Guid));

            // Assert that the response does not contain the existing event guid that has dates
            Assert.DoesNotContain(existingEventWithDates.Guid, response.Events.Select(_ => _.Guid));
        }
    }
}
