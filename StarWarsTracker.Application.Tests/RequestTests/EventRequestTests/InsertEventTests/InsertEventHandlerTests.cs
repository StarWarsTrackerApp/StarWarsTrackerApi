using StarWarsTracker.Application.BaseObjects.BaseResponses;
using StarWarsTracker.Application.BaseObjects.ExceptionResponses;
using StarWarsTracker.Application.Requests.EventRequests.Insert;
using StarWarsTracker.Domain.Enums;
using StarWarsTracker.Domain.Exceptions;
using StarWarsTracker.Persistence.DataRequestObjects.EventRequests;
using StarWarsTracker.Persistence.DataTransferObjects;
using System.Net;

namespace StarWarsTracker.Application.Tests.RequestTests.EventRequestTests.InsertEventTests
{
    public class InsertEventHandlerTests : HandlerTest
    {
        private readonly InsertEventRequest _insertEventRequest = new("Name", "Description", CanonType.StrictlyCanon);

        private readonly InsertEventHandler _handler;

        public InsertEventHandlerTests() => _handler = new(_mockDataAccess.Object, _mockLoggerFactory.Object);

        [Fact]
        public async void InsertEventHandler_Given_NameNotExisting_And_InsertEventReturnsOneRowAffected_ShouldReturn_ExecuteResponse()
        {
            SetupMockFetchAsync<IsEventNameExisting, IsEventNameExisting_DTO>(new IsEventNameExisting_DTO(false, false, false));

            SetupMockExecuteAsync<InsertEvent>(1);

            var response = await _handler.HandleRequestAsync(_insertEventRequest) as ExecuteResponse;

            Assert.NotNull(response);

            Assert.Equal((int)HttpStatusCode.OK, response.GetStatusCode());
        }

        [Fact]
        public async void InsertEventHandler_Given_NameNotExisting_And_InsertEventReturnsZeroRowsAffected_ShouldReturn_ErrorResponse()
        {
            SetupMockFetchAsync<IsEventNameExisting, IsEventNameExisting_DTO>(new IsEventNameExisting_DTO(false, false, false));

            SetupMockExecuteAsync<InsertEvent>(0);

            var response = await _handler.HandleRequestAsync(_insertEventRequest);

            Assert.IsType<ErrorResponse>(response);
        }

        [Theory]
        [InlineData(CanonType.StrictlyCanon, true, false, false)]
        [InlineData(CanonType.StrictlyCanon, false, false, true)]
        [InlineData(CanonType.StrictlyLegends, false, true, false)]
        [InlineData(CanonType.StrictlyLegends, false, false, true)]
        [InlineData(CanonType.CanonAndLegends, true, false, false)]
        [InlineData(CanonType.CanonAndLegends, false, true, false)]
        [InlineData(CanonType.CanonAndLegends, false, false, true)]
        public async void InsertEventHandler_Given_NameIsExisting_ShouldReturn_AlreadyExistsResponse(CanonType canonType, bool existsInCanon, bool existsInLegends, bool existsInCanonAndLegends)
        {
            _insertEventRequest.CanonType = canonType;

            SetupMockFetchAsync<IsEventNameExisting, IsEventNameExisting_DTO>(new IsEventNameExisting_DTO(existsInCanon, existsInLegends, existsInCanonAndLegends));

            var response = await _handler.HandleRequestAsync(_insertEventRequest);

            Assert.IsType<AlreadyExistsResponse>(response);
        }
    }
}
