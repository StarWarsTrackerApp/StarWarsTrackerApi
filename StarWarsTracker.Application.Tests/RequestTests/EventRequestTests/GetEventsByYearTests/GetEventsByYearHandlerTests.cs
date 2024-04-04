using GenFu;
using StarWarsTracker.Application.Requests.EventRequests.GetByYear;
using StarWarsTracker.Domain.Exceptions;
using StarWarsTracker.Persistence.DataRequestObjects.EventRequests;
using StarWarsTracker.Persistence.DataTransferObjects;

namespace StarWarsTracker.Application.Tests.RequestTests.EventRequestTests.GetEventsByYearTests
{
    public class GetEventsByYearHandlerTests : HandlerTest
    {
        private readonly GetEventsByYearRequest _request = new();

        private readonly GetEventsByYearHandler _handler;

        public GetEventsByYearHandlerTests() => _handler = new(_mockDataAccess.Object, _mockLogMessage.Object);

        [Fact]
        public async Task GetEventsByYear_Given_NoEventsFoundWithYear_ShouldThrow_DoesNotExistException()
        {
            SetupMockFetchListAsync<GetEventsByYear, Event_DTO>(Enumerable.Empty<Event_DTO>());

            await Assert.ThrowsAsync<DoesNotExistException>(async () => await _handler.HandleAsync(_request));
        }

        [Fact]
        public async Task GetEventsByYear_Given_EventsAreFoundWithYear_ShouldReturn_ResponseWithExpectedEvents()
        {
            var eventsDTO = A.ListOf<Event_DTO>();
            var expectedEvents = eventsDTO.Select(_ => _.AsDomainEvent());

            SetupMockFetchListAsync<GetEventsByYear, Event_DTO>(eventsDTO);

            var response = await _handler.HandleAsync(_request) as GetEventsByYearResponse;

            Assert.NotNull(response);
            Assert.Equivalent(expectedEvents, response.Events);
        }
    }
}
