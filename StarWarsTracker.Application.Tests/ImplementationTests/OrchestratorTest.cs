using Microsoft.Extensions.DependencyInjection;
using StarWarsTracker.Application.Implementation;
using StarWarsTracker.Application.Tests.ImplementationTests.TestRequests;

namespace StarWarsTracker.Application.Tests.ImplementationTests
{
    public class OrchestratorTest
    {
        [Fact]
        public async Task Orchestrator_Given_ExampleRequest_ShouldReturn_ExampleResponse()
        {
            var handlerFactory = new HandlerFactory(new ServiceCollection().BuildServiceProvider(),
                                                    new HandlerDictionary(new List<Type>() { typeof(ExampleRequestResponseHandler) }));

            var orchestrator = new Orchestrator(handlerFactory);

            var request = new ExampleRequestResponse("ExpectedMessage");

            var result = await orchestrator.SendRequest<ExampleRequestResponse, ExampleResponse>(request);

            Assert.NotNull(result);

            Assert.Equal(request.Input, result.Message);
        }
    }
}
