using StarWarsTracker.Persistence.DataRequestObjects.EventRequests;

namespace StarWarsTracker.Persistence.Tests.DataRequestTests.EventRequestTests
{
    public class DeleteEventByIdTests : DataRequestTest
    {
        [Theory]
        [InlineData(int.MinValue)]
        [InlineData(0)]
        [InlineData(int.MaxValue)]
        public async Task DeleteEventById_Given_EventNotExisting_ShouldReturn_ZeroRowsAffected(int id)
        {
            var rowsAffected = await _dataAccess.ExecuteAsync(new DeleteEventById(id));

            Assert.Equal(0, rowsAffected);
        }

        [Fact]
        public async Task DeleteEventById_Given_EventIsDeleted_ShouldReturn_OneRowAffected()
        {
            var eventExisting = await TestEvent.InsertAndFetchEventAsync();

            var rowsAffected = await _dataAccess.ExecuteAsync(new DeleteEventById(eventExisting.Id));

            // get event by name after deleting to ensure it is now null
            var eventAfterDeleting = await _dataAccess.FetchAsync(new GetEventByGuid(eventExisting.Guid));

            Assert.Equal(1, rowsAffected);

            Assert.Null(eventAfterDeleting);
        }
    }
}
