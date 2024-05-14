using GenFu;
using StarWarsTracker.Application.BaseObjects.BaseResponses;
using StarWarsTracker.Application.Requests.EventRequests.GetByYear;
using StarWarsTracker.Domain.Models;
using StarWarsTracker.Persistence.DataRequestObjects.EventRequests;
using StarWarsTracker.Persistence.DataTransferObjects;

namespace StarWarsTracker.Application.Tests.RequestTests.EventRequestTests.GetEventsByYearTests
{
    public class GetEventsByYearHandlerTests : HandlerTest
    {
        private readonly GetEventsByYearRequest _request = new();

        private readonly GetEventsByYearHandler _handler;

        public GetEventsByYearHandlerTests() => _handler = new(_mockDataAccess.Object, _mockLoggerFactory.Object);

        [Fact]
        public async Task GetEventsByYear_Given_NoEventsFoundWithYear_ShouldReturn_GetResponse_WithContent_EmptyCollectionOfEvents()
        {
            SetupMockFetchListAsync<GetEventsByYear, Event_DTO>(Enumerable.Empty<Event_DTO>());

            var response = await _handler.HandleRequestAsync(_request) as GetResponse<IEnumerable<Event>>;

            Assert.NotNull(response);

            Assert.Empty(response.Content);
        }

        [Fact]
        public async Task GetEventsByYear_Given_EventsAreFoundWithYear_ShouldReturn_GetResponse_WithContent_ExpectedEvents()
        {
            var eventsDTO = A.ListOf<Event_DTO>();
            var expectedEvents = eventsDTO.Select(_ => _.AsDomainEvent());

            SetupMockFetchListAsync<GetEventsByYear, Event_DTO>(eventsDTO);

            var response = await _handler.HandleRequestAsync(_request) as GetResponse<IEnumerable<Event>>;

            Assert.NotNull(response);

            Assert.Equivalent(expectedEvents, response.Content);
        }
    }
}
