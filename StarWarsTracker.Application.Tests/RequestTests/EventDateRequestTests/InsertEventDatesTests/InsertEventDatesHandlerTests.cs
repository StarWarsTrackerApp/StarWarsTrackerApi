using GenFu;
using StarWarsTracker.Application.Requests.EventDateRequests.Insert;
using StarWarsTracker.Application.Requests.EventRequests.GetByGuid;
using StarWarsTracker.Domain.Enums;
using StarWarsTracker.Domain.Exceptions;
using StarWarsTracker.Domain.Models;
using StarWarsTracker.Persistence.DataRequestObjects.EventDateRequests;

namespace StarWarsTracker.Application.Tests.RequestTests.EventDateRequestTests.InsertEventDatesTests
{
    public class InsertEventDatesHandlerTests : HandlerTest
    {
        private readonly InsertEventDatesRequest _request = new();

        private readonly InsertEventDatesHandler _handler;

        private readonly GetEventByGuidResponse _eventNotHavingDatesAlready = new GetEventByGuidResponse()
        {
            Event = A.New<Event>(),
            EventTimeFrame = null!
        };

        public InsertEventDatesHandlerTests() => _handler = new(_mockDataAccess.Object, _mockOrchestrator.Object);

        [Fact]
        public async Task InsertEventDates_Given_EventFoundWithGuid_HasEventTimeFrame_ShouldThrow_AlreadyExistsException()
        {
            var eventAlreadyHavingTimeFrame = new GetEventByGuidResponse()
            {
                Event = A.New<Event>(),
                EventTimeFrame = new(new EventDate(EventDateType.Definitive, 0, 0))
            };

            SetupMockGetRequestResponseAsync<GetEventByGuidRequest, GetEventByGuidResponse>(eventAlreadyHavingTimeFrame);

            await Assert.ThrowsAsync<AlreadyExistsException>(async () => await _handler.HandleAsync(_request));
        }

        [Fact]
        public async Task InsertEventDates_Given_EventFoundWithGuid_DoesNotAlreadyHaveTimeFrame_ButNoRowsImpactedDuringInsert_ShouldThrow_OperationFailedException()
        {
            // Ensure that Request is attempting to insert a date
            _request.EventDates = new EventDate[] { new EventDate(EventDateType.Definitive, 0, 0) };

            SetupMockGetRequestResponseAsync<GetEventByGuidRequest, GetEventByGuidResponse>(_eventNotHavingDatesAlready);

            SetupMockExecuteAsync<InsertEventDate>(0);

            await Assert.ThrowsAsync<OperationFailedException>(async () => await _handler.HandleAsync(_request));
        }

        [Fact]
        public async Task InsertEventDates_Given_EventDatesInserted_ShouldReturn_TaskCompletedSuccessfully()
        {
            // Ensure that Request is attempting to insert a date
            _request.EventDates = new EventDate[] { new EventDate(EventDateType.Definitive, 0, 0) };

            SetupMockGetRequestResponseAsync<GetEventByGuidRequest, GetEventByGuidResponse>(_eventNotHavingDatesAlready);

            SetupMockExecuteAsync<InsertEventDate>(1);

            var task = _handler.HandleAsync(_request);

            await task;

            Assert.True(task.IsCompletedSuccessfully);
        }

    }
}
