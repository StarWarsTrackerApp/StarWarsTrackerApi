using StarWarsTracker.Domain.Enums;
using StarWarsTracker.Persistence.DataRequestObjects.EventDateRequests;
using StarWarsTracker.Persistence.DataRequestObjects.EventRequests;
using StarWarsTracker.Persistence.Tests.TestHelpers;

namespace StarWarsTracker.Persistence.Tests.DataRequestTests.EventDateRequestTests
{
    public class GetEventDatesByEventIdTests : DataRequestTest
    {
        [Theory]
        [InlineData(int.MinValue)]
        [InlineData(0)]
        [InlineData(int.MaxValue)]

        public async Task GetEventDatesByEventId_Given_NoEventDatesWithEventId_ShouldReturn_EmptyCollection(int eventId)
        {
            Assert.Empty(await _dataAccess.FetchListAsync(new GetEventDatesByEventId(eventId)));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        [InlineData(5)]
        public async Task GetEventDatesByEventId_Given_EventDatesExistWithEventId_ShouldReturn_AllEventDates(int numberOfEventDates)
        {
            var existingEvent = await EventHelper.InsertAndFetchEventAsync(_dataAccess);

            var insertEventDateRequests = new InsertEventDate[numberOfEventDates];

            for (int i = 0; i < numberOfEventDates; i++)
            {
                var request = new InsertEventDate(existingEvent.Id, (int)EventDateType.DefinitiveStart, yearsSinceBattleOfYavin: i, sequence: i);

                insertEventDateRequests[i] = request;

                await _dataAccess.ExecuteAsync(request);
            }

            var results = await _dataAccess.FetchListAsync(new GetEventDatesByEventId(existingEvent.Id));

            await _dataAccess.ExecuteAsync(new DeleteEventDatesByEventId(existingEvent.Id));
            await _dataAccess.ExecuteAsync(new DeleteEventById(existingEvent.Id));

            Assert.Equal(numberOfEventDates, results.Count());

            // Assert that each event date exists within the results fetched with GetEventDatesByEventId
            foreach (var eventDate in insertEventDateRequests)
            {
                Assert.Single(results.Where(_ => _.YearsSinceBattleOfYavin == eventDate.YearsSinceBattleOfYavin && _.Sequence == eventDate.Sequence));
            }
        }
    }
}
