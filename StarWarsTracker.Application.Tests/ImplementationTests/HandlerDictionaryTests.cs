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
    }
}
