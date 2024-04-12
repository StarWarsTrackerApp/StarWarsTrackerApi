using GenFu;
using StarWarsTracker.Application.Requests.EventRequests.GetAllNotHavingDates;
using StarWarsTracker.Persistence.DataRequestObjects.EventRequests;
using StarWarsTracker.Persistence.DataTransferObjects;

namespace StarWarsTracker.Application.Tests.RequestTests.EventRequestTests.GetAllEventsNotHavingDatesTests
{
    public class GetAllEventsNotHavingDatesHandlerTests : HandlerTest
    {
        private readonly GetAllEventsNotHavingDatesRequest _request = new();

        private readonly GetAllEventsNotHavingDatesHandler _handler;

        public GetAllEventsNotHavingDatesHandlerTests() => _handler = new(_mockDataAccess.Object, _mockLoggerFactory.Object);

        [Fact]
        public async Task GetAllEventsNotHavingDates_Given_NoEventsFound_ShouldReturn_ResponseWithEmptyCollectionOfEvents()
        {
            SetupMockFetchListAsync<GetAllEventsNotHavingDates, Event_DTO>(Enumerable.Empty<Event_DTO>());

            var response = await _handler.HandleAsync(_request) as GetAllEventsNotHavingDatesResponse;

            Assert.NotNull(response);
            Assert.Empty(response.Events);
        }

        [Fact]
        public async Task GetAllEventsNotHavingDates_Given_EventsFound_ShouldReturn_ResponseWithExpectedEvents()
        {   
            var events = A.ListOf<Event_DTO>();
            var expected = events.Select(_ => _.AsDomainEvent());

            SetupMockFetchListAsync<GetAllEventsNotHavingDates, Event_DTO>(events);

            var response = await _handler.HandleAsync(_request) as GetAllEventsNotHavingDatesResponse;

            Assert.NotNull(response);
            Assert.Equivalent(expected, response.Events);
        }
    }
}
