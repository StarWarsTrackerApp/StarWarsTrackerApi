using StarWarsTracker.Application.Abstraction;
using StarWarsTracker.Application.Implementation;
using StarWarsTracker.Application.Tests.ImplementationTests.TestRequests;

namespace StarWarsTracker.Application.Tests.ImplementationTests
{
    public class HandlerDictionaryTests
    {
        [Fact]
        public void HandlerDictionary_Given_ExampleRequest_ShouldReturn_ExampleRequestHandler()
        {
            var request = new ExampleRequest();
            
            var expectedHandler = typeof(ExampleRequestHandler);

            var handlerDictionary = HandlerDictionary.FromAssemblyOf(expectedHandler);

            var result = handlerDictionary.TryGetHandlerType(request, out var handler);

            Assert.True(result);

            Assert.Equal(expectedHandler, handler);
        }

        [Fact]
        public void HandlerDictionary_Given_DefaultConstructorCalled_GetHandlerType_ShouldReturn_HandlerForAllRequests()
        {
            // Get all request classes from the Application assembly that implement IRequest
            var requests = typeof(HandlerDictionary).Assembly.GetTypes().Where(_ => (typeof(IHandler<>).IsAssignableFrom(_)) && _.IsClass);

            var handlerDictionary = HandlerDictionary.FromDictionaryAssembly();

            var handlersFound = requests.Select(r => handlerDictionary.TryGetHandlerType(r, out _));

            // Assert that every request was able to return a handler
            Assert.True(handlersFound.All(handlerFound => handlerFound == true));
        }
    }
}
