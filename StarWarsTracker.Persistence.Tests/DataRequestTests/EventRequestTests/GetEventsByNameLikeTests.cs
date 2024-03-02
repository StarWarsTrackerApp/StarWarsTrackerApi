using StarWarsTracker.Persistence.DataRequestObjects.EventRequests;

namespace StarWarsTracker.Persistence.Tests.DataRequestTests.EventRequestTests
{
    public class GetEventsByNameLikeTests : DataRequestTest
    {
        [Fact]
        public async Task GetEventsByNameLike_Given_NoEventsFoundWithName_ShouldReturn_EmptyCollection()
        {
            var results = await _dataAccess.FetchListAsync(new GetEventsByNameLike(TestString.Random()));

            Assert.Empty(results);
        }

        [Fact]
        public async Task GetEventsByNameLike_Given_EventsFoundWithName_ShouldReturn_MatchingEvents()
        {
            var partOfNameToSearchBy = TestString.Random();

            var firstEventWithName = await TestEvent.InsertAndFetchEventAsync(name: partOfNameToSearchBy + TestString.Random());

            var secondEventWithName = await TestEvent.InsertAndFetchEventAsync(name: TestString.Random() + partOfNameToSearchBy);

            var thirdEventWithoutName = await TestEvent.InsertAndFetchEventAsync();

            var results = await _dataAccess.FetchListAsync(new GetEventsByNameLike(partOfNameToSearchBy));

            // Delete the inserted events
            await _dataAccess.ExecuteAsync(new DeleteEventById(firstEventWithName.Id));
            await _dataAccess.ExecuteAsync(new DeleteEventById(secondEventWithName.Id));
            await _dataAccess.ExecuteAsync(new DeleteEventById(thirdEventWithoutName.Id));

            // Assert that the third event without the name to search by was not returned
            Assert.DoesNotContain(thirdEventWithoutName.Id, results.Select(_ => _.Id));

            var firstEventFromResponse = results.Single(_ => _.Id == firstEventWithName.Id);
            var secondEventFromResponse = results.Single(_ => _.Id == secondEventWithName.Id);

            // Assert that first event has correct values
            Assert.Equal(firstEventWithName.Id, firstEventFromResponse.Id);
            Assert.Equal(firstEventWithName.Guid, firstEventFromResponse.Guid);
            Assert.Equal(firstEventWithName.Name, firstEventFromResponse.Name);
            Assert.Equal(firstEventWithName.Description, firstEventFromResponse.Description);
            Assert.Equal(firstEventWithName.CanonTypeId, firstEventFromResponse.CanonTypeId);

            // Assert that second event has correct values
            Assert.Equal(secondEventWithName.Id, secondEventFromResponse.Id);
            Assert.Equal(secondEventWithName.Guid, secondEventFromResponse.Guid);
            Assert.Equal(secondEventWithName.Name, secondEventFromResponse.Name);
            Assert.Equal(secondEventWithName.Description, secondEventFromResponse.Description);
            Assert.Equal(secondEventWithName.CanonTypeId, secondEventFromResponse.CanonTypeId);
        }
    }
}
