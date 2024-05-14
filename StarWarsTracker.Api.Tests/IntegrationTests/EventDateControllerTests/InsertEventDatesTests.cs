using StarWarsTracker.Api.Tests.TestHelpers;
using StarWarsTracker.Application.BaseObjects.ExceptionResponses;
using StarWarsTracker.Application.Requests.EventDateRequests.Insert;
using StarWarsTracker.Domain.Exceptions;
using StarWarsTracker.Domain.Models;
using StarWarsTracker.Persistence.DataRequestObjects.EventDateRequests;
using StarWarsTracker.Persistence.DataRequestObjects.EventRequests;
using StarWarsTracker.Tests.Shared.Helpers;

namespace StarWarsTracker.Api.Tests.IntegrationTests.EventDateControllerTests
{
    public class InsertEventDatesTests : ControllerTest<EventDateController>
    {
        [Fact]
        public async Task InsertEventDate_Given_EventNotExistingWithGuid_ShouldReturn_NotFoundResponse_With_NameOfObjectNotExisting_Event()
        {
            var expectedNameOfObjectNotExisting = nameof(Event);

            var request = new InsertEventDatesRequest(Guid.NewGuid(), new EventDate[] { new(Domain.Enums.EventDateType.Definitive, 1, 0) });

            var result = await _controller.InsertEventDates(request);

            var responseBody = result.GetResponseBody<NotFoundResponse>();

            Assert.Equal(StatusCodes.Status404NotFound, result.GetStatusCode());

            Assert.NotNull(responseBody);

            Assert.Equal(expectedNameOfObjectNotExisting, responseBody.NameOfObjectNotExisting);
        }

        [Fact]
        public async Task InsertEventDate_Given_EventExistsWithGuid_ButEventAlreadyHasDates_ShouldReturn_NotFoundResponse_With_NameOfObjectNotExisting_EventDate()
        {
            var expectedNameOfObjectAlreadyExisting = nameof(EventTimeFrame);

            var (eventAlreadyHavingEventDate, _) = await TestEventDate.InsertAndFetchEventDateAsync();

            var request = new InsertEventDatesRequest(eventAlreadyHavingEventDate.Guid, new EventDate[] { new(Domain.Enums.EventDateType.Definitive, 1, 0) });

            var result = await _controller.InsertEventDates(request);

            await TestDataAccess.SharedInstance.ExecuteAsync(new DeleteEventDatesByEventId(eventAlreadyHavingEventDate.Id));
            await TestDataAccess.SharedInstance.ExecuteAsync(new DeleteEventById(eventAlreadyHavingEventDate.Id));

            var responseBody = result.GetResponseBody<AlreadyExistsResponse>();

            Assert.Equal(StatusCodes.Status409Conflict, result.GetStatusCode());
            
            Assert.NotNull(responseBody);

            Assert.Equal(expectedNameOfObjectAlreadyExisting, responseBody.NameOfObjectAlreadyExisting);
        }

        //TODO: Bad Request and Happy Path testing
    }
}
