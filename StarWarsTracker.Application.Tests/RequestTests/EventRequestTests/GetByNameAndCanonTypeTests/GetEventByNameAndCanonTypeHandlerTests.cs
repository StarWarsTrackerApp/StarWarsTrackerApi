using StarWarsTracker.Application.Requests.EventRequests.GetByNameAndCanonType;
using StarWarsTracker.Domain.Enums;
using StarWarsTracker.Domain.Exceptions;
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
        public async Task GetEventByNameAndCanonType_Given_NoEventFound_ShouldThrow_DoesNotExistException()
        {
            SetupMockFetchAsync<GetEventByNameAndCanonType, Event_DTO>(null);

            await Assert.ThrowsAsync<DoesNotExistException>(async () => await _handler.HandleAsync(_request));
        }

        [Fact]
        public async Task GetEventByNameAndCanonType_Given_EventFound_ShouldReturn_Event()
        {
            var expected = new Event_DTO()
            {
                Guid = Guid.NewGuid(),
                Name = TestString.Random(),
                Description = TestString.Random(),
                CanonTypeId = (int)CanonType.CanonAndLegends
            };

            SetupMockFetchAsync<GetEventByNameAndCanonType, Event_DTO>(expected);

            var result = await _handler.HandleAsync(_request) as GetEventByNameAndCanonTypeResponse;

            Assert.NotNull(result);

            Assert.Equal(expected.Guid, result.Event.Guid);
            Assert.Equal(expected.Name, result.Event.Name);
            Assert.Equal(expected.Description, result.Event.Description);
            Assert.Equal(expected.CanonTypeId, (int)result.Event.CanonType);

        }
    }
}
