using GenFu;
using StarWarsTracker.Application.Requests.EventDateRequests.Delete;
using StarWarsTracker.Domain.Exceptions;
using StarWarsTracker.Domain.Models;
using StarWarsTracker.Persistence.DataRequestObjects.EventDateRequests;
using StarWarsTracker.Persistence.DataRequestObjects.EventRequests;
using StarWarsTracker.Persistence.DataTransferObjects;

namespace StarWarsTracker.Application.Tests.RequestTests.EventDateRequestTests.DeleteEventDatesByEventGuidTests
{
    public class DeleteEventDatesByEventGuidHandlerTests : HandlerTest
    {
        private readonly DeleteEventDatesByEventGuidRequest _request = new();

        private readonly DeleteEventDatesByEventGuidHandler _handler;

        public DeleteEventDatesByEventGuidHandlerTests() => _handler = new(_mockDataAccess.Object, _mockLogMessage.Object);

        [Fact]
        public async Task DeleteEventDatesByEventGuid_Given_NoEventFoundWithGuid_ShouldThrow_DoesNotExistException_WithEventNotFound()
        {
            SetupMockFetchAsync<GetEventByGuid, Event_DTO>(null);

            var expectedNameOfObjectNotExisting = nameof(Event);

            var exception = await Record.ExceptionAsync(async () => await _handler.HandleAsync(_request)) as DoesNotExistException;

            Assert.NotNull(exception);

            Assert.Equal(expectedNameOfObjectNotExisting, exception.NameOfObjectNotExisting);
        }

        [Fact]
        public async Task DeleteEventDatesByEventGuid_Given_EventFoundWithGuid_ButNoEventDatesFound_ShouldThrow_DoesNotExistException_WithEventDateNotFound()
        {
            SetupMockFetchAsync<GetEventByGuid, Event_DTO>(A.New<Event_DTO>());

            SetupMockFetchListAsync<GetEventDatesByEventId, EventDate_DTO>(Enumerable.Empty<EventDate_DTO>());

            var expectedNameOfObjectNotExisting = nameof(EventDate);

            var exception = await Record.ExceptionAsync(async () => await _handler.HandleAsync(_request)) as DoesNotExistException;

            Assert.NotNull(exception);

            Assert.Equal(expectedNameOfObjectNotExisting, exception.NameOfObjectNotExisting);
        }

        [Theory]
        [InlineData(1, 0)]
        [InlineData(1, 2)]
        [InlineData(5, 0)]
        [InlineData(5, 10)]
        public async Task DeleteEventDatesByEventGuid_Given_EventDatesFound_ButNumberOfDatesDeleted_NotSameCountAsEventDatesFound_ShouldThrow_OperationFailedException(int numberOfDates, int numberOfDatesDeleted)
        {
            SetupMockFetchAsync<GetEventByGuid, Event_DTO>(A.New<Event_DTO>());

            SetupMockFetchListAsync<GetEventDatesByEventId, EventDate_DTO>(A.ListOf<EventDate_DTO>(numberOfDates));

            SetupMockExecuteAsync<DeleteEventDatesByEventId>(numberOfDatesDeleted);

            await Assert.ThrowsAsync<OperationFailedException>(async () => await _handler.HandleAsync(_request));
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

            var task = _handler.HandleAsync(_request);

            await task;

            Assert.True(task.IsCompletedSuccessfully);
        }
    }
}
