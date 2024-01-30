using StarWarsTracker.Persistence.DataRequestObjects.EventRequests;

namespace StarWarsTracker.Persistence.Tests.DataRequestTests.EventRequestTests
{
    public class InsertEventTests : DataRequestTest
    {
        [Fact]
        public async Task InsertEvent_Given_EventIsInserted_ShouldReturn_OneRowsAffected()
        {
            var insertEventRequest = TestEvent.NewInsertEvent();

            // execute insert request and get number of rows affected
            var rowsAffectedDuringInsert = await _dataAccess.ExecuteAsync(insertEventRequest);

            // fetch the record inserted to ensure it exists
            var recordInserted = await _dataAccess.FetchAsync(new GetEventByGuid(insertEventRequest.Guid));

            // Delete inserted row
            await _dataAccess.ExecuteAsync(new DeleteEventById(recordInserted!.Id));

            Assert.Equal(1, rowsAffectedDuringInsert);

            Assert.NotNull(recordInserted);
        }

        [Fact]
        public async Task InsertEvent_Given_GuidAlreadyExists_ShouldReturn_NegativeOneRowsAffected()
        {
            // insert event with name so that it already exists
            var existingEvent = await TestEvent.InsertAndFetchEventAsync();

            // now attempt to insert again with the same name
            var rowsAffected = await _dataAccess.ExecuteAsync(TestEvent.NewInsertEvent(guid: existingEvent.Guid));

            // Delete inserted row
            await _dataAccess.ExecuteAsync(new DeleteEventById(existingEvent.Id));

            Assert.Equal(-1, rowsAffected);
        }
    }
}
