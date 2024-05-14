using StarWarsTracker.Api.Tests.TestHelpers;
using StarWarsTracker.Application.Requests.EventRequests.GetByYear;
using StarWarsTracker.Domain.Models;
using StarWarsTracker.Tests.Shared.Helpers;

namespace StarWarsTracker.Api.Tests.IntegrationTests.EventControllerTests
{
    public class GetEventsByYearTests : ControllerTest<EventController>
    {
        [Fact]
        public async Task GetEventsByYear_Given_NoEventsExistWithYear_ShouldReturn_SuccessResponse_With_EmptyCollection()
        {
            var request = new GetEventsByYearRequest() { YearsSinceBattleOfYavin = int.MinValue };

            var result = await _controller.GetEventsByYear(request);

            var eventsFound = result.GetResponseBody<IEnumerable<Event>>();

            Assert.Equal(StatusCodes.Status200OK, result.GetStatusCode());

            Assert.NotNull(eventsFound);

            Assert.Empty(eventsFound);
        }

        [Fact]
        public async Task GetEventsByYear_Given_EventsExistWithDatesDuringYear_ShouldReturn_SuccessResponse_With_EventsOccuringDuringThatYear()
        {
            var year = 24;

            var (firstEventDuringYear, _) = await TestEventDate.InsertAndFetchEventDateAsync(yearsSincleBattleOfYavin: year);

            var (secondEventDuringYear, _) = await TestEventDate.InsertAndFetchEventDateAsync(yearsSincleBattleOfYavin: year);

            var (thirdEventDuringDifferentYear, _) = await TestEventDate.InsertAndFetchEventDateAsync(yearsSincleBattleOfYavin: year + 1);

            var request = new GetEventsByYearRequest() { YearsSinceBattleOfYavin = year };

            var result = await _controller.GetEventsByYear(request);

            var eventsFound = result.GetResponseBody<IEnumerable<Event>>();

            Assert.Equal(StatusCodes.Status200OK, result.GetStatusCode());

            Assert.NotNull(eventsFound);

            // Delete inserted objects
            await _controller.DeleteEvent(new(firstEventDuringYear.Guid));
            await _controller.DeleteEvent(new(secondEventDuringYear.Guid));
            await _controller.DeleteEvent(new(thirdEventDuringDifferentYear.Guid));

            // Assert that third event during a different year was not returned
            Assert.DoesNotContain(thirdEventDuringDifferentYear.Guid, eventsFound.Select(_ => _.Guid));

            var firstEventResponse = eventsFound.Single(_ => _.Guid == firstEventDuringYear.Guid);
            var secondEventResponse = eventsFound.Single(_ => _.Guid == secondEventDuringYear.Guid);

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
        public async Task GetEventsByYear_Given_Null_ShouldReturn_BadRequestResponse()
        {
            var result = await _controller.GetEventsByYear(null!);

            Assert.Equal(StatusCodes.Status400BadRequest, result.GetStatusCode());
        }
    }
}
