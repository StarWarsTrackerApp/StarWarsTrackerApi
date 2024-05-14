using GenFu;
using StarWarsTracker.Application.BaseObjects.BaseResponses;
using StarWarsTracker.Application.Requests.EventRequests.GetAllNotHavingDates;
using StarWarsTracker.Domain.Models;
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
        public async Task GetAllEventsNotHavingDates_Given_NoEventsFound_ShouldReturn_GetResponse_WithContent_EmptyCollectionOfEvents()
        {
            SetupMockFetchListAsync<GetAllEventsNotHavingDates, Event_DTO>(Enumerable.Empty<Event_DTO>());

            var response = await _handler.HandleRequestAsync(_request) as GetResponse<IEnumerable<Event>>;

            Assert.NotNull(response);

            Assert.Empty(response.Content);
        }

        [Fact]
        public async Task GetAllEventsNotHavingDates_Given_EventsFound_ShouldReturn_GetResponse_WithContent_ExpectedEvents()
        {   
            var events = A.ListOf<Event_DTO>();
            var expected = events.Select(_ => _.AsDomainEvent());

            SetupMockFetchListAsync<GetAllEventsNotHavingDates, Event_DTO>(events);

            var response = await _handler.HandleRequestAsync(_request) as GetResponse<IEnumerable<Event>>;

            Assert.NotNull(response);

            Assert.Equivalent(expected, response.Content);
        }
    }
}
