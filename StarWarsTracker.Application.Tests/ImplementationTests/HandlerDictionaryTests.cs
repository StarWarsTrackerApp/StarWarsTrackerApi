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
            var handlerDictionary = new HandlerDictionary(new List<Type>() { typeof(ExampleRequestResponseHandler) });

            var result = handlerDictionary.GetHandlerType(typeof(ExampleRequestResponse));

            Assert.NotNull(result);

            Assert.Equal(typeof(ExampleRequestResponseHandler), result);
        }

        [Fact]
        public void HandlerDictionary_Given_DefaultConstructorCalled_GetHandlerType_ShouldReturn_HandlerForAllRequests()
        {
            // Get all request classes from the Application assembly that implement IRequest
            var requests = typeof(HandlerDictionary).Assembly.GetTypes().Where(_ => (typeof(IRequest).IsAssignableFrom(_)) && _.IsClass);

            var handlerDictionary = new HandlerDictionary();

            var handlersFound = requests.Select(_ => handlerDictionary.GetHandlerType(_));

            // Assert that every request was able to return a handler
            Assert.True(handlersFound.All(_ => _ is not null));
        }
    }
}
