using StarWarsTracker.Domain.Enums;
using StarWarsTracker.Persistence.DataRequestObjects.EventDateRequests;
using StarWarsTracker.Persistence.DataRequestObjects.EventRequests;

namespace StarWarsTracker.Persistence.Tests.DataRequestTests.EventRequestTests
{
    public class GetEventsByYearTests : DataRequestTest
    {
        [Fact]
        public async Task GetEventByYear_Given_NoEventExistsWithYear_ShouldReturn_EmptyCollection()
        {
            Assert.Empty(await _dataAccess.FetchListAsync(new GetEventsByYear(int.MinValue)));
        }

        [Fact]
        public async Task GetEventByYear_Given_EventsExistDuringYear_ShouldReturn_EventsExistingDuringThatYear()
        {
            var year = 45;

            var firstEvent = await TestEvent.InsertAndFetchEventAsync();
            await _dataAccess.ExecuteAsync(new InsertEventDate(firstEvent.Guid, (int)EventDateType.Definitive, year, 0));

            var secondEvent = await TestEvent.InsertAndFetchEventAsync();
            await _dataAccess.ExecuteAsync(new InsertEventDate(secondEvent.Guid, (int)EventDateType.Definitive, year, 1));

            var results = await _dataAccess.FetchListAsync(new GetEventsByYear(year));

            await _dataAccess.ExecuteAsync(new DeleteEventDatesByEventId(firstEvent.Id));
            await _dataAccess.ExecuteAsync(new DeleteEventById(firstEvent.Id));

            await _dataAccess.ExecuteAsync(new DeleteEventDatesByEventId(secondEvent.Id));
            await _dataAccess.ExecuteAsync(new DeleteEventById(secondEvent.Id));

            Assert.NotEmpty(results);

            var resultsGuids = results.Select(x => x.Guid);

            Assert.Contains(firstEvent.Guid, resultsGuids);
            Assert.Contains(secondEvent.Guid, resultsGuids);
        }

        [Fact]
        public async Task GetEventByYear_Given_EventsExistDuringYear_ShouldReturn_EventsOrderedBySequence()
        {
            var year = -45;

            // insert them out of order just to make sure we are ordering by sequence
            var secondChronologicalEvent = await TestEvent.InsertAndFetchEventAsync();
            await _dataAccess.ExecuteAsync(new InsertEventDate(secondChronologicalEvent.Guid, (int)EventDateType.Definitive, year, sequence: 10));

            var firstChronologicalEvent = await TestEvent.InsertAndFetchEventAsync();
            await _dataAccess.ExecuteAsync(new InsertEventDate(firstChronologicalEvent.Guid, (int)EventDateType.Definitive, year, sequence: 5));

            // store results in a List so we can get index of events
            var results = (await _dataAccess.FetchListAsync(new GetEventsByYear(year))).ToList();

            await _dataAccess.ExecuteAsync(new DeleteEventDatesByEventId(secondChronologicalEvent.Id));
            await _dataAccess.ExecuteAsync(new DeleteEventById(secondChronologicalEvent.Id));

            await _dataAccess.ExecuteAsync(new DeleteEventDatesByEventId(firstChronologicalEvent.Id));
            await _dataAccess.ExecuteAsync(new DeleteEventById(firstChronologicalEvent.Id));

            Assert.NotEmpty(results);

            var firstEvent = results.First(_ => _.Guid == firstChronologicalEvent.Guid);
            var secondEvent = results.First(_ => _.Guid == secondChronologicalEvent.Guid);

            Assert.True(results.IndexOf(firstEvent) < results.IndexOf(secondEvent));
        }
    }
}
