using StarWarsTracker.Persistence.DataRequestObjects.EventDateRequests;
using StarWarsTracker.Persistence.DataRequestObjects.EventRequests;

namespace StarWarsTracker.Persistence.Tests.DataRequestTests.EventRequestTests
{
    public class GetAllEventsNotHavingDatesTests : DataRequestTest
    {
        [Fact]
        public async Task GetAllEventsNotHavingDates_Given_ExistingEventHasDates_ShouldReturn_CollectionNotContainingEvent()
        {
            var (existingEventWithDates, _) = await TestEventDate.InsertAndFetchEventDateAsync();

            var results = await _dataAccess.FetchListAsync(new GetAllEventsNotHavingDates());

            await _dataAccess.ExecuteAsync(new DeleteEventDatesByEventId(existingEventWithDates.Id));
            await _dataAccess.ExecuteAsync(new DeleteEventById(existingEventWithDates.Id));

            Assert.DoesNotContain(existingEventWithDates.Id, results.Select(_ => _.Id));
        }

        [Fact]
        public async Task GetAllEventsNotHavingDates_Given_ExistingEventHasNoDates_ShouldReturn_CollectionContainingEvent()
        {
            var existingEventWithoutDates = await TestEvent.InsertAndFetchEventAsync();

            var results = await _dataAccess.FetchListAsync(new GetAllEventsNotHavingDates());

            await _dataAccess.ExecuteAsync(new DeleteEventById(existingEventWithoutDates.Id));

            var eventReturned = results.Single(_ => _.Id == existingEventWithoutDates.Id);

            //Assert that the eventReturned is not null
            Assert.NotNull(eventReturned);

            // Assert that the properties on the event match
            Assert.Equal(existingEventWithoutDates.Id, eventReturned.Id);
            Assert.Equal(existingEventWithoutDates.Guid, eventReturned.Guid);
            Assert.Equal(existingEventWithoutDates.Name, eventReturned.Name);
            Assert.Equal(existingEventWithoutDates.Description, eventReturned.Description);
            Assert.Equal(existingEventWithoutDates.CanonTypeId, eventReturned.CanonTypeId);
        }
    }
}
