using StarWarsTracker.Persistence.DataRequestObjects.EventRequests;
using StarWarsTracker.Persistence.Tests.TestHelpers;

namespace StarWarsTracker.Persistence.Tests.DataRequestTests.EventRequestTests
{
    public class GetEventByNameTests : DataRequestTest
    {
        [Fact]
        public async Task GetEventByName_Given_EventNotExisting_ShouldReturn_Null()
        {
            Assert.Null(await _dataAccess.FetchAsync(new GetEventByName(StringHelper.RandomString())));
        }

        [Fact]
        public async Task GetEventByName_Given_EventIsExisting_ShouldReturn_Event()
        {
            var insertEventRequest = EventHelper.NewInsertEvent();

            await _dataAccess.ExecuteAsync(insertEventRequest);

            var result = await _dataAccess.FetchAsync(new GetEventByName(insertEventRequest.Name));

            Assert.NotNull(result);

            await _dataAccess.ExecuteAsync(new DeleteEventById(result.Id));
            
            Assert.Equal(insertEventRequest.Name, result.Name);
            Assert.Equal(insertEventRequest.Description, result.Description);
            Assert.Equal(insertEventRequest.IsCanon, result.IsCannon);
        }
    }
}
