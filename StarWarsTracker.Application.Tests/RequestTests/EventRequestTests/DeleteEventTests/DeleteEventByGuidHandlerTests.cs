using StarWarsTracker.Application.BaseObjects.BaseResponses;
using StarWarsTracker.Application.Requests.EventRequests.Delete;
using StarWarsTracker.Domain.Exceptions;
using StarWarsTracker.Persistence.DataRequestObjects.EventRequests;
using StarWarsTracker.Persistence.DataTransferObjects;
using System.Net;

namespace StarWarsTracker.Application.Tests.RequestTests.EventRequestTests.DeleteEventTests
{
    public class DeleteEventByGuidHandlerTests : HandlerTest
    {
        private readonly DeleteEventByGuidRequest _request = new DeleteEventByGuidRequest(Guid.NewGuid());

        private readonly DeleteEventByGuidHandler _handler;

        public DeleteEventByGuidHandlerTests() => _handler = new(_mockDataAccess.Object, _mockLoggerFactory.Object);

        [Fact]
        public async Task DeleteEventByGuid_Given_NoEventFoundWithGuid_ShouldReturn_NotFoundResponse()
        {
            SetupMockFetchAsync<GetEventByGuid, Event_DTO>(null);

            var response = await _handler.HandleRequestAsync(_request);

            Assert.IsType<NotFoundResponse>(response);
        }

        [Fact]
        public async Task DeleteEventByGuid_Given_EventFoundWithGuid_ButNoRowsImpactedWhenDeleting_ShouldReturn_ErrorResponse()
        {
            SetupMockFetchAsync<GetEventByGuid, Event_DTO>(new Event_DTO());

            SetupMockExecuteAsync<DeleteEventById>(0);

            var response = await _handler.HandleRequestAsync(_request);

            Assert.IsType<ErrorResponse>(response);
        }

        [Fact]
        public async Task DeleteEventByGuid_Given_EventFoundWithGuid_AndRowIsImpactedWhenDeleting_ShouldReturn_ExecuteResponse()
        {
            SetupMockFetchAsync<GetEventByGuid, Event_DTO>(new Event_DTO());

            SetupMockExecuteAsync<DeleteEventById>(1);

            var response = await _handler.HandleRequestAsync(_request);

            Assert.IsType<ExecuteResponse>(response);

            Assert.Equal((int)HttpStatusCode.OK, response.GetStatusCode());
        }
    }
}
