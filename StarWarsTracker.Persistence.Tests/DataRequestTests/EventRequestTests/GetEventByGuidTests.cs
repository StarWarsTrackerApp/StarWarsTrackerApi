using StarWarsTracker.Persistence.DataRequestObjects.EventRequests;

namespace StarWarsTracker.Persistence.Tests.DataRequestTests.EventRequestTests
{
    public class GetEventByGuidTests : DataRequestTest
    {
        [Fact]
        public async Task GetEventByGuid_Given_EventNotExisting_ShouldReturn_Null()
        {
            Assert.Null(await _dataAccess.FetchAsync(new GetEventByGuid(Guid.NewGuid())));
        }

        [Fact]
        public async Task GetEventByGuid_Given_EventIsExisting_ShouldReturn_Event()
        {
            var insertEventRequest = TestEvent.NewInsertEvent();

            await _dataAccess.ExecuteAsync(insertEventRequest);

            var result = await _dataAccess.FetchAsync(new GetEventByGuid(insertEventRequest.Guid));

            Assert.NotNull(result);

            await _dataAccess.ExecuteAsync(new DeleteEventById(result.Id));

            Assert.Equal(insertEventRequest.Name, result.Name);
            Assert.Equal(insertEventRequest.Description, result.Description);
            Assert.Equal(insertEventRequest.CanonTypeId, result.CanonTypeId);
        }
    }
}
