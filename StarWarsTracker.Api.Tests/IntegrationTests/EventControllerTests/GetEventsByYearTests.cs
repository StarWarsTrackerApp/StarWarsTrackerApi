using StarWarsTracker.Application.Requests.EventRequests.GetByYear;
using StarWarsTracker.Domain.Exceptions;
using StarWarsTracker.Tests.Shared.Helpers;

namespace StarWarsTracker.Api.Tests.IntegrationTests.EventControllerTests
{
    public class GetEventsByYearTests : ControllerTest<EventController>
    {
        [Fact]
        public async Task GetEventsByYear_Given_NoEventsExistWithYear_ShouldThrow_DoesNotExistException()
        {
            var request = new GetEventsByYearRequest() { YearsSinceBattleOfYavin = int.MinValue };

            await Assert.ThrowsAsync<DoesNotExistException>(async () => await _controller.GetEventsByYear(request));
        }

        [Fact]
        public async Task GetEventsByYear_Given_EventsExistWithDatesDuringYear_ShouldReturn_EventsOccuringDuringThatYear()
        {
            var year = 24;

            var (firstEventDuringYear, _) = await TestEventDate.InsertAndFetchEventDateAsync(yearsSincleBattleOfYavin: year);

            var (secondEventDuringYear, _) = await TestEventDate.InsertAndFetchEventDateAsync(yearsSincleBattleOfYavin: year);

            var (thirdEventDuringDifferentYear, _) = await TestEventDate.InsertAndFetchEventDateAsync(yearsSincleBattleOfYavin: year + 1);

            var request = new GetEventsByYearRequest() { YearsSinceBattleOfYavin = year };

            var response = await _controller.GetEventsByYear(request);

            await _controller.DeleteEvent(new(firstEventDuringYear.Guid));
            await _controller.DeleteEvent(new(secondEventDuringYear.Guid));
            await _controller.DeleteEvent(new(thirdEventDuringDifferentYear.Guid));

            // Assert that third event during a different year was not returned
            Assert.DoesNotContain(thirdEventDuringDifferentYear.Guid, response.Events.Select(_ => _.Guid));

            var firstEventResponse = response.Events.Single(_ => _.Guid == firstEventDuringYear.Guid);
            var secondEventResponse = response.Events.Single(_ => _.Guid == secondEventDuringYear.Guid);

            // Assert that first event was returned with correct values
            Assert.NotNull(firstEventResponse);
            Assert.Equal(firstEventDuringYear.Guid, firstEventResponse.Guid);
            Assert.Equal(firstEventDuringYear.Name, firstEventResponse.Name);
            Assert.Equal(firstEventDuringYear.Description, firstEventResponse.Description);
            Assert.Equal(firstEventDuringYear.CanonTypeId, (int)firstEventResponse.CanonType);

            // Assert that second event was returned with correct values
            Assert.NotNull(firstEventResponse);
            Assert.Equal(secondEventDuringYear.Guid, secondEventResponse.Guid);
            Assert.Equal(secondEventDuringYear.Name, secondEventResponse.Name);
            Assert.Equal(secondEventDuringYear.Description, secondEventResponse.Description);
            Assert.Equal(secondEventDuringYear.CanonTypeId, (int)secondEventResponse.CanonType);
        }

        [Fact]
        public async Task GetEventsByYear_Given_Null_ShouldThrow_ValidationFailedException()
        {
            await Assert.ThrowsAsync<ValidationFailureException>(async () => await _controller.GetEventsByYear(null!));
        }
    }
}
