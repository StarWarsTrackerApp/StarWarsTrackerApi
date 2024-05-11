using GenFu;
using StarWarsTracker.Application.BaseObjects.BaseResponses;
using StarWarsTracker.Application.Requests.EventDateRequests.Delete;
using StarWarsTracker.Domain.Exceptions;
using StarWarsTracker.Domain.Models;
using StarWarsTracker.Persistence.DataRequestObjects.EventDateRequests;
using StarWarsTracker.Persistence.DataRequestObjects.EventRequests;
using StarWarsTracker.Persistence.DataTransferObjects;
using System.Net;

namespace StarWarsTracker.Application.Tests.RequestTests.EventDateRequestTests.DeleteEventDatesByEventGuidTests
{
    public class DeleteEventDatesByEventGuidHandlerTests : HandlerTest
    {
        private readonly DeleteEventDatesByEventGuidRequest _request = new();

        private readonly DeleteEventDatesByEventGuidHandler _handler;

        public DeleteEventDatesByEventGuidHandlerTests() => _handler = new(_mockDataAccess.Object, _mockLoggerFactory.Object);

        [Fact]
        public async Task DeleteEventDatesByEventGuid_Given_NoEventFoundWithGuid_ShouldReturn_NotFoundResponse_WithNameOfObjectNotFound_Event()
        {
            SetupMockFetchAsync<GetEventByGuid, Event_DTO>(null);

            var expectedNameOfObjectNotExisting = nameof(Event);

            var response = await _handler.HandleRequestAsync(_request) as NotFoundResponse;

            Assert.NotNull(response);

            Assert.Equal(expectedNameOfObjectNotExisting, response.NameOfObjectNotExisting);
        }

        [Fact]
        public async Task DeleteEventDatesByEventGuid_Given_EventFoundWithGuid_ButNoEventDatesFound_ShouldReturn_NotFoundResponse_WithNameOfObjectNotFound_EventDate()
        {
            SetupMockFetchAsync<GetEventByGuid, Event_DTO>(A.New<Event_DTO>());

            SetupMockFetchListAsync<GetEventDatesByEventId, EventDate_DTO>(Enumerable.Empty<EventDate_DTO>());

            var expectedNameOfObjectNotExisting = nameof(EventDate);

            var response = await _handler.HandleRequestAsync(_request) as NotFoundResponse;

            Assert.NotNull(response);

            Assert.Equal(expectedNameOfObjectNotExisting, response.NameOfObjectNotExisting);
        }

        [Theory]
        [InlineData(1, 0)]
        [InlineData(1, 2)]
        [InlineData(5, 0)]
        [InlineData(5, 10)]
        public async Task DeleteEventDatesByEventGuid_Given_EventDatesFound_ButNumberOfDatesDeleted_NotSameCountAsEventDatesFound_ShouldReturn_ErrorResponse(int numberOfDates, int numberOfDatesDeleted)
        {
            SetupMockFetchAsync<GetEventByGuid, Event_DTO>(A.New<Event_DTO>());

            SetupMockFetchListAsync<GetEventDatesByEventId, EventDate_DTO>(A.ListOf<EventDate_DTO>(numberOfDates));

            SetupMockExecuteAsync<DeleteEventDatesByEventId>(numberOfDatesDeleted);

            var result = await _handler.HandleRequestAsync(_request);

            Assert.IsType<ErrorResponse>(result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public async Task DeleteEventDatesByEventGuid_Given_EventDatesDeleted_ShouldReturn_TaskCompletedSuccessfully(int numberOfDates)
        {
            SetupMockFetchAsync<GetEventByGuid, Event_DTO>(A.New<Event_DTO>());

            SetupMockFetchListAsync<GetEventDatesByEventId, EventDate_DTO>(A.ListOf<EventDate_DTO>(numberOfDates));

            SetupMockExecuteAsync<DeleteEventDatesByEventId>(numberOfDates);

            var response = await _handler.HandleRequestAsync(_request) as ExecuteResponse;

            Assert.NotNull(response);

            Assert.Equal((int)HttpStatusCode.OK, response.GetStatusCode());
        }
    }
}
