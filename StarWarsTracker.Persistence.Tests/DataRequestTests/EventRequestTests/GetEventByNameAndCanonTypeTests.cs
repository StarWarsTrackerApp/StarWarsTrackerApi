using StarWarsTracker.Domain.Enums;
using StarWarsTracker.Persistence.DataRequestObjects.EventRequests;

namespace StarWarsTracker.Persistence.Tests.DataRequestTests.EventRequestTests
{
    public class GetEventByNameAndCanonTypeTests : DataRequestTest
    {
        [Fact]
        public async Task GetEventByNameAndCanonType_Given_NoEventFoundWithName_ShouldReturn_Null()
        {
            var result = await _dataAccess.FetchAsync(new GetEventByNameAndCanonType(TestString.Random(), (int)CanonType.CanonAndLegends));

            Assert.Null(result);
        }

        [Fact]
        public async Task GetEventByNameAndCanonType_Given_EventExistsWithCanonType_ShouldReturn_ExistingEvent()
        {
            var existingEvent = await TestEvent.InsertAndFetchEventAsync();

            var result = await _dataAccess.FetchAsync(new GetEventByNameAndCanonType(existingEvent.Name, existingEvent.CanonTypeId));

            await _dataAccess.ExecuteAsync(new DeleteEventById(existingEvent.Id));

            Assert.NotNull(result);

            Assert.Equal(existingEvent.Id, result.Id);
            Assert.Equal(existingEvent.Guid, result.Guid);
            Assert.Equal(existingEvent.Name, result.Name);
            Assert.Equal(existingEvent.Description, result.Description);
            Assert.Equal(existingEvent.CanonTypeId, result.CanonTypeId);
        }
    }
}
