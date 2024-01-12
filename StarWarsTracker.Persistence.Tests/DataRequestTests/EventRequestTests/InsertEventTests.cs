using StarWarsTracker.Persistence.DataRequestObjects.EventRequests;
using StarWarsTracker.Persistence.Tests.TestHelpers;

namespace StarWarsTracker.Persistence.Tests.DataRequestTests.EventRequestTests
{
    public class InsertEventTests : DataRequestTest
    {
        [Fact]
        public async Task InsertEvent_Given_EventIsInserted_ShouldReturn_OneRowsAffected()
        {
            var insertEventRequest = EventHelper.NewInsertEvent();

            // execute insert request and get number of rows affected
            var rowsAffectedDuringInsert = await _dataAccess.ExecuteAsync(insertEventRequest);

            // fetch the record inserted to ensure it exists
            var recordInserted = await _dataAccess.FetchAsync(new GetEventByName(insertEventRequest.Name));

            // Delete inserted row
            await _dataAccess.ExecuteAsync(new DeleteEventById(rowsAffectedDuringInsert));

            Assert.Equal(1, rowsAffectedDuringInsert);

            Assert.NotNull(recordInserted);
        }

        [Fact]
        public async Task InsertEvent_Given_EventNameAlreadyExists_ShouldReturn_NegativeOneRowsAffected()
        {
            var eventNameAlreadyTaken = "EventNameAlreadyTaken";

            // insert event with name so that it already exists
            var existingEvent = await EventHelper.InsertAndFetchEventAsync(_dataAccess, name: eventNameAlreadyTaken);

            // now attempt to insert again with the same name
            var rowsAffected = await _dataAccess.ExecuteAsync(EventHelper.NewInsertEvent(name: eventNameAlreadyTaken));

            // Delete inserted row
            await _dataAccess.ExecuteAsync(new DeleteEventById(existingEvent.Id));

            Assert.Equal(-1, rowsAffected);
        }
    }
}
