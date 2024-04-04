using GenFu;
using StarWarsTracker.Application.Requests.EventRequests.GetByGuid;
using StarWarsTracker.Domain.Exceptions;
using StarWarsTracker.Domain.Models;
using StarWarsTracker.Persistence.DataRequestObjects.EventDateRequests;
using StarWarsTracker.Persistence.DataRequestObjects.EventRequests;
using StarWarsTracker.Persistence.DataTransferObjects;

namespace StarWarsTracker.Application.Tests.RequestTests.EventRequestTests.GetEventByGuidTests
{
    public class GetEventByGuidHandlerTests : HandlerTest
    {
        private readonly GetEventByGuidRequest _request = new();

        private readonly GetEventByGuidHandler _handler;

        public GetEventByGuidHandlerTests() => _handler = new(_mockDataAccess.Object, _mockLogMessage.Object);

        [Fact]
        public async Task GetEventByGuid_Given_NoEventFoundWithGuid_ShouldThrow_DoesNotExistException()
        {
            SetupMockFetchAsync<GetEventByGuid, Event_DTO>(null);

            await Assert.ThrowsAsync<DoesNotExistException>(async () => await _handler.HandleAsync(_request));
        }

        [Fact]
        public async Task GetEventByGuid_Given_EventFoundWithoutEventDates_ShouldReturn_ResponseWithEvent_AndNullTimeFrame()
        {
            var eventDTO = A.New<Event_DTO>();
            var expectedEvent = eventDTO.AsDomainEvent();

            SetupMockFetchAsync<GetEventByGuid, Event_DTO>(eventDTO);

            SetupMockFetchListAsync<GetEventDatesByEventId, EventDate_DTO>(Enumerable.Empty<EventDate_DTO>());

            var response = await _handler.HandleAsync(_request) as GetEventByGuidResponse;

            Assert.NotNull(response);
            Assert.Equivalent(expectedEvent, response.Event);
            Assert.Null(response.EventTimeFrame);
        }

        [Fact]
        public async Task GetEventByGuid_Given_EventFoundWithEventDates_ShouldReturn_ResponseWithEvent_AndTimeFrame()
        {
            var eventDTO = A.New<Event_DTO>();
            var expectedEvent = eventDTO.AsDomainEvent();

            var eventDates = A.ListOf<EventDate_DTO>();
            var expectedTimeFrame = new EventTimeFrame(eventDates.Select(_ => _.AsDomainEventDate()));

            SetupMockFetchAsync<GetEventByGuid, Event_DTO>(eventDTO);

            SetupMockFetchListAsync<GetEventDatesByEventId, EventDate_DTO>(eventDates);

            var response = await _handler.HandleAsync(_request) as GetEventByGuidResponse;

            Assert.NotNull(response);
            Assert.Equivalent(expectedEvent, response.Event);
            Assert.Equivalent(expectedTimeFrame, response.EventTimeFrame);
        }
    }
}
