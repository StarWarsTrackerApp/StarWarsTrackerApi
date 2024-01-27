using StarWarsTracker.Application.Requests.EventRequests.Delete;
using StarWarsTracker.Domain.Exceptions;
using StarWarsTracker.Persistence.DataRequestObjects.EventRequests;
using StarWarsTracker.Persistence.DataTransferObjects;

namespace StarWarsTracker.Application.Tests.RequestTests.EventRequestTests.DeleteEventTests
{
    public class DeleteEventByGuidHandlerTests : HandlerTest
    {
        private readonly DeleteEventByGuidRequest _request = new DeleteEventByGuidRequest(Guid.NewGuid());

        private readonly DeleteEventByGuidHandler _handler;

        public DeleteEventByGuidHandlerTests() => _handler = new(_mockDataAccess.Object);

        [Fact]
        public async Task DeleteEventByGuid_Given_NoEventFoundWithGuid_ShouldThrow_DoesNotExistException()
        {
            SetupMockFetchAsync<GetEventByGuid, Event_DTO>(null);

            await Assert.ThrowsAsync<DoesNotExistException>(async () => await _handler.HandleAsync(_request));
        }

        [Fact]
        public async Task DeleteEventByGuid_Given_EventFoundWithGuid_ButNoRowsImpactedWhenDeleting_ShouldThrow_OperationFailedException()
        {
            SetupMockFetchAsync<GetEventByGuid, Event_DTO>(new Event_DTO());

            SetupMockExecuteAsync<DeleteEventById>(0);

            await Assert.ThrowsAsync<OperationFailedException>(async () => await _handler.HandleAsync(_request));
        }

        [Fact]
        public async Task DeleteEventByGuid_Given_EventFoundWithGuid_AndRowIsImpactedWhenDeleting_ShouldReturn_TaskCompletedSuccessfully()
        {
            SetupMockFetchAsync<GetEventByGuid, Event_DTO>(new Event_DTO());

            SetupMockExecuteAsync<DeleteEventById>(1);

            var task = _handler.HandleAsync(_request);

            await task;

            Assert.True(task.IsCompletedSuccessfully);
        }
    }
}
