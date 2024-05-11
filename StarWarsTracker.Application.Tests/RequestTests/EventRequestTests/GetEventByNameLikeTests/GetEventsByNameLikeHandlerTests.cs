using GenFu;
using StarWarsTracker.Application.BaseObjects.BaseResponses;
using StarWarsTracker.Application.Requests.EventRequests.GetByNameLike;
using StarWarsTracker.Domain.Models;
using StarWarsTracker.Persistence.DataRequestObjects.EventRequests;
using StarWarsTracker.Persistence.DataTransferObjects;

namespace StarWarsTracker.Application.Tests.RequestTests.EventRequestTests.GetEventByNameLikeTests
{
    public class GetEventsByNameLikeHandlerTests : HandlerTest
    {
        private readonly GetEventsByNameLikeRequest _request = new();

        private readonly GetEventsByNameLikeHandler _handler;

        public GetEventsByNameLikeHandlerTests() => _handler = new(_mockDataAccess.Object, _mockLoggerFactory.Object);

        [Fact]
        public async Task GetEventsByNameLike_Given_NoEventsFoundWithName_ShouldReturn_GetResponse_WithContent_EmptyCollectionOfEvents()
        {
            SetupMockFetchListAsync<GetEventsByNameLike, Event_DTO>(Enumerable.Empty<Event_DTO>());

            var response = await _handler.HandleRequestAsync(_request) as GetResponse<IEnumerable<Event>>;

            Assert.NotNull(response);

            Assert.Empty(response.Content);
        }

        [Fact]
        public async Task GetEventsByNameLike_Given_EventsAreFoundWithName_ShouldReturn_GetResponse_WithContent_ExpectedEvents()
        {
            var eventsDTO = A.ListOf<Event_DTO>();
            var expectedEvents = eventsDTO.Select(_ => _.AsDomainEvent());

            SetupMockFetchListAsync<GetEventsByNameLike, Event_DTO>(eventsDTO);

            var response = await _handler.HandleRequestAsync(_request) as GetResponse<IEnumerable<Event>>;

            Assert.NotNull(response);

            Assert.Equivalent(expectedEvents, response.Content);
        }
    }
}
