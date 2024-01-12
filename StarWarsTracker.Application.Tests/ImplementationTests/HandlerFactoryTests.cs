using Moq;
using StarWarsTracker.Application.Abstraction;
using StarWarsTracker.Application.Implementation;
using StarWarsTracker.Application.Tests.ImplementationTests.TestRequests;
using StarWarsTracker.Domain.Exceptions;

namespace StarWarsTracker.Application.Tests.ImplementationTests
{
    public class HandlerFactoryTests
    {
        private readonly HandlerFactory _handlerFactory;

        private readonly Mock<IHandlerDictionary> _mockHandlerDictionary;

        public HandlerFactoryTests()
        {
            _mockHandlerDictionary = new Mock<IHandlerDictionary>();

            _handlerFactory = new HandlerFactory(new Mock<IServiceProvider>().Object, _mockHandlerDictionary.Object);
        }

        [Fact]
        public void HandlerFactory_Given_HandlerDictionaryReturnsHandler_ShouldReturn_InstantiatedHandler()
        {
            var request = new ExampleRequest();
            
            _mockHandlerDictionary.Setup(_ => _.GetHandlerType(request.GetType())).Returns(typeof(ExampleRequestHandler));

            var result = _handlerFactory.NewRequestHandler(new ExampleRequest());

            Assert.NotNull(result);
        }

        [Fact]
        public void HandlerFactory_Given_HandlerNotExisting_ShouldThrow_DoesNotExistException()
        {
            Assert.Throws<DoesNotExistException>(() => _handlerFactory.NewRequestHandler(new ExampleRequest()));
        }
    }
}
