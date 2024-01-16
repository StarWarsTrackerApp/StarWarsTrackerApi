using StarWarsTracker.Domain.Enums;
using StarWarsTracker.Persistence.DataRequestObjects.EventDateRequests;
using StarWarsTracker.Persistence.DataRequestObjects.EventRequests;
using StarWarsTracker.Persistence.Tests.TestHelpers;
using System.Data.SqlClient;

namespace StarWarsTracker.Persistence.Tests.DataRequestTests.EventDateRequestTests
{
    public class InsertEventDateTests : DataRequestTest
    {
        [Fact]
        public async Task InsertEventDate_Given_GuidNotExisting_ShouldThrow_SqlException()
        {
            var guidNotExisting = Guid.NewGuid();

            var request = new InsertEventDate(guidNotExisting, (int)EventDateType.Definitive, 0, 0);

            await Assert.ThrowsAsync<SqlException>(async () => await _dataAccess.ExecuteAsync(request));
        }

        [Theory]
        [InlineData(int.MinValue)]
        [InlineData(0)]
        [InlineData(int.MaxValue)]
        public async Task InsertEventDate_Given_EventDateTypeIdNotExisting_ShouldThrow_SqlException(int eventDateTypeId)
        {
            var existingEvent = await EventHelper.InsertAndFetchEventAsync(_dataAccess);

            var request = new InsertEventDate(existingEvent.Guid, eventDateTypeId, 0, 0);

            var exception = await Record.ExceptionAsync(async () => await _dataAccess.ExecuteAsync(request));

            await _dataAccess.ExecuteAsync(new DeleteEventById(existingEvent.Id));

            Assert.IsType<SqlException>(exception);
        }

        [Fact]
        public async Task InsertEventDate_Given_EventDateIsInserted_ShouldReturn_OneRowAffected()
        {
            var existingEvent = await EventHelper.InsertAndFetchEventAsync(_dataAccess);

            var insertEventDateRequest = new InsertEventDate(existingEvent.Guid, (int)EventDateType.Definitive, 123, 321);

            var rowsAffected = await _dataAccess.ExecuteAsync(insertEventDateRequest);

            var eventDateInserted = await _dataAccess.FetchAsync(new GetEventDatesByEventId(existingEvent.Id));

            await _dataAccess.ExecuteAsync(new DeleteEventDatesByEventId(existingEvent.Id));
            await _dataAccess.ExecuteAsync(new DeleteEventById(existingEvent.Id));

            Assert.Equal(1, rowsAffected);
            Assert.NotNull(eventDateInserted);

            Assert.Equal(existingEvent.Id, eventDateInserted.EventId);

            Assert.Equal(insertEventDateRequest.EventDateTypeId, eventDateInserted.EventDateTypeId);
            Assert.Equal(insertEventDateRequest.YearsSinceBattleOfYavin, eventDateInserted.YearsSinceBattleOfYavin);
            Assert.Equal(insertEventDateRequest.Sequence, eventDateInserted.Sequence);
        }
    }
}
