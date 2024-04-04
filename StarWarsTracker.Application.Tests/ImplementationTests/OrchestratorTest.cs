using Moq;
using StarWarsTracker.Application.Abstraction;
using StarWarsTracker.Application.Implementation;
using StarWarsTracker.Application.Tests.ImplementationTests.TestRequests;
using StarWarsTracker.Domain.Logging;

namespace StarWarsTracker.Application.Tests.ImplementationTests
{
    public class OrchestratorTest
    {
        private readonly IOrchestrator _orchestrator;

        private readonly Mock<IHandlerFactory> _mockHandlerFactory = new();

        private readonly Mock<ILogMessage> _mockLogMessage = new();

        public OrchestratorTest()
        {
            _orchestrator = new Orchestrator(_mockHandlerFactory.Object, _mockLogMessage.Object);
        }

        [Fact]
        public async Task Orchestrator_Given_ExampleRequestResponse_ShouldReturn_ExampleResponse()
        {
            var request = new ExampleRequestResponse("Request Message");
            var expected = new ExampleResponse("Expected Response");

            var mockHandler = new Mock<IBaseHandler>();
            mockHandler.Setup(_ => _.HandleAsync(It.IsAny<object>())).Returns(Task.FromResult((object?)expected));

            _mockHandlerFactory.Setup(_ => _.NewHandler(It.IsAny<object>())).Returns(mockHandler.Object);

            var result = await _orchestrator.GetRequestResponseAsync(request);

            Assert.NotNull(result);
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task Orchestrator_Given_ExampleRequest_ShouldReturn_TaskCompleted()
        {
            var request = new ExampleRequest();

            var mockHandler = new Mock<IBaseHandler>();
            mockHandler.Setup(_ => _.HandleAsync(It.IsAny<object>())).Returns(Task.FromResult((object?)null));

            _mockHandlerFactory.Setup(_ => _.NewHandler(It.IsAny<object>())).Returns(mockHandler.Object);

            var e = _orchestrator.ExecuteRequestAsync(request);

            await e;

            Assert.True(e.IsCompleted);
        }
    }
}
