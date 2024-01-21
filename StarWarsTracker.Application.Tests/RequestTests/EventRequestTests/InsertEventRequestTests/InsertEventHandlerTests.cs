using StarWarsTracker.Application.Requests.EventRequests.InsertEventRequests;
using StarWarsTracker.Domain.Enums;
using StarWarsTracker.Domain.Exceptions;
using StarWarsTracker.Persistence.DataRequestObjects.EventRequests;
using StarWarsTracker.Persistence.DataTransferObjects;

namespace StarWarsTracker.Application.Tests.RequestTests.EventRequestTests.InsertEventRequestTests
{
    public class InsertEventHandlerTests : HandlerTest
    {
        private readonly InsertEventRequest _insertEventRequest = new("Name", "Description", CanonType.StrictlyCanon);

        private readonly InsertEventHandler _handler;

        public InsertEventHandlerTests() => _handler = new(_mockDataAccess.Object);

        [Fact]
        public async void InsertEventHandler_Given_NameNotExisting_And_InsertEventReturnsOneRowAffected_ShouldReturn_CompletedTask()
        {
            SetupMockFetchAsync<IsEventNameExisting, IsEventNameExisting_DTO>(new IsEventNameExisting_DTO(false, false, false));

            SetupMockExecuteAsync<InsertEvent>(1);

            var task = _handler.ExecuteRequestAsync(_insertEventRequest);

            await task;

            Assert.True(task.IsCompleted);
        }

        [Fact]
        public async void InsertEventHandler_Given_NameNotExisting_And_InsertEventReturnsZeroRowsAffected_ShouldThrow_OperationFailedException()
        {
            SetupMockFetchAsync<IsEventNameExisting, IsEventNameExisting_DTO>(new IsEventNameExisting_DTO(false, false, false));

            SetupMockExecuteAsync<InsertEvent>(0);

            await Assert.ThrowsAsync<OperationFailedException>(async () => await _handler.ExecuteRequestAsync(_insertEventRequest));
        }

        [Theory]
        [InlineData(CanonType.StrictlyCanon, true, false, false)]
        [InlineData(CanonType.StrictlyCanon, false, false, true)]
        [InlineData(CanonType.StrictlyLegends, false, true, false)]
        [InlineData(CanonType.StrictlyLegends, false, false, true)]
        [InlineData(CanonType.CanonAndLegends, true, false, false)]
        [InlineData(CanonType.CanonAndLegends, false, true, false)]
        [InlineData(CanonType.CanonAndLegends, false, false, true)]
        public async void InsertEventHandler_Given_NameIsExisting_ShouldThrow_AlreadyExistsException(CanonType canonType, bool existsInCanon, bool existsInLegends, bool existsInCanonAndLegends)
        {
            _insertEventRequest.CanonType = canonType;

            SetupMockFetchAsync<IsEventNameExisting, IsEventNameExisting_DTO>(new IsEventNameExisting_DTO(existsInCanon, existsInLegends, existsInCanonAndLegends));

            await Assert.ThrowsAsync<AlreadyExistsException>(async () => await _handler.ExecuteRequestAsync(_insertEventRequest));
        }
    }
}
