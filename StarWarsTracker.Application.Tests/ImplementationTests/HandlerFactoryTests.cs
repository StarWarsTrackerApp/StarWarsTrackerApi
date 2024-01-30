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

        private readonly Mock<ITypeActivator> _mockTypeActivator;

        public HandlerFactoryTests()
        {
            _mockHandlerDictionary = new Mock<IHandlerDictionary>();

            _mockTypeActivator = new Mock<ITypeActivator>();

            _handlerFactory = new HandlerFactory(_mockTypeActivator.Object, _mockHandlerDictionary.Object);
        }

        [Fact]
        public void HandlerFactory_Given_HandlerDictionaryReturnsHandler_ShouldReturn_HandlerInstantiatedByActivator()
        {
            var request = new ExampleRequest();
            var handlerType = typeof(ExampleRequestHandler);
            
            _mockHandlerDictionary.Setup(_ => _.GetHandlerType(request.GetType())).Returns(handlerType);

            _mockTypeActivator.Setup(_ => _.Instantiate<IBaseHandler>(handlerType)).Returns(new ExampleRequestHandler());

            var result = _handlerFactory.NewHandler(new ExampleRequest());

            Assert.NotNull(result);
        }

        [Fact]
        public void HandlerFactory_Given_HandlerNotExisting_ShouldThrow_DoesNotExistException()
        {
            Assert.Throws<DoesNotExistException>(() => _handlerFactory.NewHandler(new ExampleRequest()));
        }

        [Fact]
        public void HandlerFactory_Given_Null_ShouldThrow_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _handlerFactory.NewHandler((object)null!));
        }
    }
}
