using StarWarsTracker.Application.BaseObjects.BaseResponses;
using StarWarsTracker.Application.Requests.EventRequests.GetByNameAndCanonType;
using StarWarsTracker.Domain.Enums;
using StarWarsTracker.Domain.Exceptions;
using StarWarsTracker.Domain.Models;
using StarWarsTracker.Persistence.DataRequestObjects.EventRequests;
using StarWarsTracker.Persistence.DataTransferObjects;
using StarWarsTracker.Tests.Shared.Helpers;

namespace StarWarsTracker.Application.Tests.RequestTests.EventRequestTests.GetByNameAndCanonTypeTests
{
    public class GetEventByNameAndCanonTypeHandlerTests : HandlerTest
    {
        private readonly GetEventByNameAndCanonTypeRequest _request = new();

        private readonly GetEventByNameAndCanonTypeHandler _handler;

        public GetEventByNameAndCanonTypeHandlerTests() => _handler = new(_mockDataAccess.Object, _mockLoggerFactory.Object);

        [Fact]
        public async Task GetEventByNameAndCanonType_Given_NoEventFound_ShouldReturn_NotFoundResponse()
        {
            SetupMockFetchAsync<GetEventByNameAndCanonType, Event_DTO>(null);

            var response = await _handler.HandleRequestAsync(_request);

            Assert.IsType<NotFoundResponse>(response);
        }

        [Fact]
        public async Task GetEventByNameAndCanonType_Given_EventFound_ShouldReturn_GetResponse_WithContent_ExpectedEvent()
        {
            var expected = new Event_DTO()
            {
                Guid = Guid.NewGuid(),
                Name = TestString.Random(),
                Description = TestString.Random(),
                CanonTypeId = (int)CanonType.CanonAndLegends
            };

            SetupMockFetchAsync<GetEventByNameAndCanonType, Event_DTO>(expected);

            var result = await _handler.HandleRequestAsync(_request) as GetResponse<Event>;

            Assert.NotNull(result);

            Assert.Equal(expected.Guid, result.Content.Guid);
            Assert.Equal(expected.Name, result.Content.Name);
            Assert.Equal(expected.Description, result.Content.Description);
            Assert.Equal(expected.CanonTypeId, (int)result.Content.CanonType);
        }
    }
}
