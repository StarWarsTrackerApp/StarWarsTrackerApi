using StarWarsTracker.Persistence.DataRequestObjects.EventDateRequests;
using StarWarsTracker.Persistence.DataRequestObjects.EventRequests;
using StarWarsTracker.Persistence.Tests.TestHelpers;
using StarWarsTracker.Tests.Shared.Helpers;

namespace StarWarsTracker.Persistence.Tests.DataRequestTests.EventRequestTests
{
    public class IsEventNameExistingTests : DataRequestTest
    {
        [Fact]
        public async Task IsEventExisting_Given_NoEventsWithName_ShouldReturn_AllBooleansFalse()
        {
            var result = await _dataAccess.FetchAsync(new IsEventNameExisting(StringHelper.RandomString()));

            Assert.NotNull(result);

            Assert.False(result.NameExistsInStrictlyCanon);
            Assert.False(result.NameExistsInStrictlyLegends);
            Assert.False(result.NameExistsInCanonAndLegends);
        }

        [Fact]
        public async Task IsEventExisting_Given_StrictlyCanonEventExisting_ShouldReturn_NameExistsInStrictlyCanon_True()
        {
            var eventName = StringHelper.RandomString();

            var existingEvent = await EventHelper.InsertAndFetchEventAsync(_dataAccess, name: eventName, canonType: Domain.Enums.CanonType.StrictlyCanon);

            var result = await _dataAccess.FetchAsync(new IsEventNameExisting(eventName));

            await _dataAccess.ExecuteAsync(new DeleteEventById(existingEvent.Id));

            Assert.NotNull(result);

            Assert.True(result.NameExistsInStrictlyCanon);
            Assert.False(result.NameExistsInStrictlyLegends);
            Assert.False(result.NameExistsInCanonAndLegends);
        }

        [Fact]
        public async Task IsEventExisting_Given_StrictlyLegendsEventExisting_ShouldReturn_NameExistsInStrictlyLegends_True()
        {
            var eventName = StringHelper.RandomString();

            var existingEvent = await EventHelper.InsertAndFetchEventAsync(_dataAccess, name: eventName, canonType: Domain.Enums.CanonType.StrictlyLegends);

            var result = await _dataAccess.FetchAsync(new IsEventNameExisting(eventName));

            await _dataAccess.ExecuteAsync(new DeleteEventById(existingEvent.Id));

            Assert.NotNull(result);

            Assert.True(result.NameExistsInStrictlyLegends);
            Assert.False(result.NameExistsInStrictlyCanon);
            Assert.False(result.NameExistsInCanonAndLegends);
        }

        [Fact]
        public async Task IsEventExisting_Given_CanonAndLegendsEventExisting_ShouldReturn_NameExistsInCanonAndLegends_True()
        {
            var eventName = StringHelper.RandomString();

            var existingEvent = await EventHelper.InsertAndFetchEventAsync(_dataAccess, name: eventName, canonType: Domain.Enums.CanonType.CanonAndLegends);

            var result = await _dataAccess.FetchAsync(new IsEventNameExisting(eventName));

            await _dataAccess.ExecuteAsync(new DeleteEventById(existingEvent.Id));

            Assert.NotNull(result);

            Assert.True(result.NameExistsInCanonAndLegends);
            Assert.False(result.NameExistsInStrictlyLegends);
            Assert.False(result.NameExistsInStrictlyCanon);
        }

        [Fact]
        public async Task IsEventExisting_Given_StrictlyCanonAndStrictlyLegendsEventExists_ShouldReturn_NameExistsInStrictlyCanonAndStrictlyLegends_True()
        {
            var eventName = StringHelper.RandomString();

            var canonEvent = await EventHelper.InsertAndFetchEventAsync(_dataAccess, name: eventName, canonType: Domain.Enums.CanonType.StrictlyCanon);
            var legendsEvent = await EventHelper.InsertAndFetchEventAsync(_dataAccess, name: eventName, canonType: Domain.Enums.CanonType.StrictlyLegends);

            var result = await _dataAccess.FetchAsync(new IsEventNameExisting(eventName));

            await _dataAccess.ExecuteAsync(new DeleteEventById(canonEvent.Id));
            await _dataAccess.ExecuteAsync(new DeleteEventById(legendsEvent.Id));

            Assert.NotNull(result);

            Assert.True(result.NameExistsInStrictlyLegends);
            Assert.True(result.NameExistsInStrictlyCanon);
            Assert.False(result.NameExistsInCanonAndLegends);
        }
    }
}
