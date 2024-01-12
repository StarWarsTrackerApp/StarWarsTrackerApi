﻿using StarWarsTracker.Persistence.DataRequestObjects.EventRequests;
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

            // delete inserted row
            await _dataAccess.ExecuteAsync(new DeleteEventById(rowsAffectedDuringInsert));

            Assert.Equal(1, rowsAffectedDuringInsert);

            Assert.NotNull(recordInserted);
        }

        [Fact]
        public async Task InsertEvent_Given_EventNameAlreadyExists_ShouldReturn_NegativeOneRowsAffected()
        {
            var eventNameAlreadyTaken = "EventNameAlreadyTaken";
            
            // insert event with name so that it already exists
            await _dataAccess.ExecuteAsync(EventHelper.NewInsertEvent(name: eventNameAlreadyTaken));
            
            // get the event so that we can delete by id
            var eventInserted = await _dataAccess.FetchAsync(new GetEventByName(eventNameAlreadyTaken));

            // now attempt to insert again with the same name
            var rowsAffectedDuringInsertWhenNameIsTaken = await _dataAccess.ExecuteAsync(EventHelper.NewInsertEvent(name: eventNameAlreadyTaken));

            await _dataAccess.ExecuteAsync(new DeleteEventById(eventInserted!.Id));

            Assert.Equal(-1, rowsAffectedDuringInsertWhenNameIsTaken);
        }
    }
}