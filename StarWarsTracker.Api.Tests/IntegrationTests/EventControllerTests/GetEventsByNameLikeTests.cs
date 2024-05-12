using StarWarsTracker.Api.Tests.TestHelpers;
using StarWarsTracker.Domain.Models;
using StarWarsTracker.Tests.Shared.Helpers;

namespace StarWarsTracker.Api.Tests.IntegrationTests.EventControllerTests
{
    public class GetEventsByNameLikeTests : ControllerTest<EventController>
    {
        [Fact]
        public async Task GetEventsByNameLike_Given_NoEventsFoundWithName_ShouldReturn_SuccessResponse_With_EmptyCollection()
        {
            var result = await _controller.GetEventsByNameLike(new(TestString.Random()));

            var eventsFound = result.GetResponseBody<IEnumerable<Event>>();

            Assert.Equal(StatusCodes.Status200OK, result.GetStatusCode());

            Assert.NotNull(eventsFound);

            Assert.Empty(eventsFound);
        }

        [Fact]
        public async Task GetEventsByNameLike_Given_EventsFoundWithName_ShouldReturn_SuccessResponse_With_MatchingEvents()
        {
            var partOfNameToSearchBy = TestString.Random();

            var firstEventWithName = await TestEvent.InsertAndFetchEventAsync(name: partOfNameToSearchBy + TestString.Random());

            var secondEventWithName = await TestEvent.InsertAndFetchEventAsync(name: TestString.Random() + partOfNameToSearchBy);

            var thirdEventWithoutName = await TestEvent.InsertAndFetchEventAsync();

            var result = await _controller.GetEventsByNameLike(new(partOfNameToSearchBy));

            var eventsFound = result.GetResponseBody<IEnumerable<Event>>();

            Assert.Equal(StatusCodes.Status200OK, result.GetStatusCode());

            Assert.NotNull(eventsFound);

            // Delete the inserted events
            await _controller.DeleteEvent(new(firstEventWithName.Guid));
            await _controller.DeleteEvent(new(secondEventWithName.Guid));
            await _controller.DeleteEvent(new(thirdEventWithoutName.Guid));

            // Assert that the third event without the name to search by was not returned
            Assert.DoesNotContain(thirdEventWithoutName.Guid, eventsFound.Select(_ => _.Guid));

            var firstEventFromResponse = eventsFound.Single(_ => _.Guid == firstEventWithName.Guid);
            var secondEventFromResponse = eventsFound.Single(_ => _.Guid == secondEventWithName.Guid);

            // Assert that first event has correct values
            Assert.Equal(firstEventWithName.Guid, firstEventFromResponse.Guid);
            Assert.Equal(firstEventWithName.Name, firstEventFromResponse.Name);
            Assert.Equal(firstEventWithName.Description, firstEventFromResponse.Description);
            Assert.Equal(firstEventWithName.CanonTypeId, (int)firstEventFromResponse.CanonType);

            // Assert that second event has correct values
            Assert.Equal(secondEventWithName.Guid, secondEventFromResponse.Guid);
            Assert.Equal(secondEventWithName.Name, secondEventFromResponse.Name);
            Assert.Equal(secondEventWithName.Description, secondEventFromResponse.Description);
            Assert.Equal(secondEventWithName.CanonTypeId, (int)secondEventFromResponse.CanonType);
        }

    }
}
