using StarWarsTracker.Api.Tests.TestHelpers;
using StarWarsTracker.Domain.Models;
using StarWarsTracker.Tests.Shared.Helpers;

namespace StarWarsTracker.Api.Tests.IntegrationTests.EventControllerTests
{
    public class GetAllEventsNotHavingDatesTests : ControllerTest<EventController>
    {
        [Fact]
        public async Task GetAllEventsNotHavingDates_Given_EventInsertedHasNoDates_ShouldReturn_SuccessResponse_With_EventInsertedWithoutDates()
        {
            var existingEventWithoutDates = await TestEvent.InsertAndFetchEventAsync();

            var result = await _controller.GetAllEventsNotHavingDates();

            var eventsFound = result.GetResponseBody<IEnumerable<Event>>();

            await _controller.DeleteEvent(new(existingEventWithoutDates.Guid));

            Assert.Equal(StatusCodes.Status200OK, result.GetStatusCode());

            Assert.NotNull(eventsFound);

            var eventReturned = eventsFound.Single(_ => _.Guid == existingEventWithoutDates.Guid);

            //Assert that the eventReturned is not null
            Assert.NotNull(eventReturned);

            // Assert that the properties on the event match
            Assert.Equal(existingEventWithoutDates.Guid, eventReturned.Guid);
            Assert.Equal(existingEventWithoutDates.Name, eventReturned.Name);
            Assert.Equal(existingEventWithoutDates.Description, eventReturned.Description);
            Assert.Equal(existingEventWithoutDates.CanonTypeId, (int)eventReturned.CanonType);
        }

        [Fact]
        public async Task GetAllEventsNotHavingDates_Given_EventInsertedHasDates_ShouldReturn_SuccessResponse_Without_EventInsertedWithDates()
        {
            var (existingEventWithDates, _) = await TestEventDate.InsertAndFetchEventDateAsync();

            var response = await _controller.GetAllEventsNotHavingDates();

            await _controller.DeleteEvent(new(existingEventWithDates.Guid));

            Assert.Equal(StatusCodes.Status200OK, response.GetStatusCode());

            var eventsFound = response.GetResponseBody<IEnumerable<Event>>();

            Assert.NotNull(eventsFound);

            Assert.DoesNotContain(existingEventWithDates.Guid, eventsFound.Select(_ => _.Guid));
        }
    }
}
