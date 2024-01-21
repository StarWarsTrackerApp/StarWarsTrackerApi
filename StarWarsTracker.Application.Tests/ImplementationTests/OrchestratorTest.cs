using Microsoft.Extensions.DependencyInjection;
using StarWarsTracker.Application.Abstraction;
using StarWarsTracker.Application.Implementation;
using StarWarsTracker.Application.Tests.ImplementationTests.TestRequests;

namespace StarWarsTracker.Application.Tests.ImplementationTests
{
    public class OrchestratorTest
    {
        private readonly IOrchestrator _orchestrator;

        public OrchestratorTest()
        {
            var handlerFactory = new HandlerFactory(new TypeActivator(new ServiceCollection().BuildServiceProvider()),
                                                   new HandlerDictionary(new List<Type>() { typeof(ExampleRequestResponseHandler), typeof(ExampleRequestHandler) }));

            _orchestrator = new Orchestrator(handlerFactory);
        }

        [Fact]
        public async Task Orchestrator_Given_ExampleRequestResponse_ShouldReturn_ExampleResponse()
        {
            var request = new ExampleRequestResponse("ExpectedMessage");

            var result = await _orchestrator.GetRequestResponseAsync<ExampleRequestResponse, ExampleResponse>(request);

            Assert.NotNull(result);
            Assert.Equal(request.Input, result.Message);
        }

        [Fact]
        public async Task Orchestrator_Given_ExampleRequest_ShouldReturn_TaskCompleted()
        {
            var request = new ExampleRequest();

            var e = _orchestrator.ExecuteRequestAsync(request);

            await e;

            Assert.True(e.IsCompleted);
        }
    }
}
