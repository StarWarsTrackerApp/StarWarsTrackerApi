using GenFu;
using StarWarsTracker.Application.Requests.EventRequests.GetByNameLike;
using StarWarsTracker.Domain.Exceptions;
using StarWarsTracker.Persistence.DataRequestObjects.EventRequests;
using StarWarsTracker.Persistence.DataTransferObjects;

namespace StarWarsTracker.Application.Tests.RequestTests.EventRequestTests.GetEventByNameLikeTests
{
    public class GetEventsByNameLikeHandlerTests : HandlerTest
    {
        private readonly GetEventsByNameLikeRequest _request = new();

        private readonly GetEventsByNameLikeHandler _handler;

        public GetEventsByNameLikeHandlerTests() => _handler = new(_mockDataAccess.Object, _mockLogMessage.Object);

        [Fact]
        public async Task GetEventsByNameLike_Given_NoEventsFoundWithName_ShouldThrow_DoesNotExistException()
        {
            SetupMockFetchListAsync<GetEventsByNameLike, Event_DTO>(Enumerable.Empty<Event_DTO>());

            await Assert.ThrowsAsync<DoesNotExistException>(async () => await _handler.HandleAsync(_request));
        }

        [Fact]
        public async Task GetEventsByNameLike_Given_EventsAreFoundWithName_ShouldReturn_ResponseWithExpectedEvents()
        {
            var eventsDTO = A.ListOf<Event_DTO>();
            var expectedEvents = eventsDTO.Select(_ => _.AsDomainEvent());

            SetupMockFetchListAsync<GetEventsByNameLike, Event_DTO>(eventsDTO);

            var response = await _handler.HandleAsync(_request) as GetEventsByNameLikeResponse;

            Assert.NotNull(response);
            Assert.Equivalent(expectedEvents, response.Events);
        }
    }
}
