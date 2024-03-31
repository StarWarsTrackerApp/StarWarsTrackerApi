using Moq;
using StarWarsTracker.Application.Abstraction;
using StarWarsTracker.Application.Implementation;
using StarWarsTracker.Application.Tests.ImplementationTests.TestRequests;
using StarWarsTracker.Domain.Exceptions;
using StarWarsTracker.Domain.Logging;

namespace StarWarsTracker.Application.Tests.ImplementationTests
{
    public class HandlerFactoryTests
    {
        #region Private Members

        private readonly HandlerFactory _handlerFactory;

        private readonly Mock<IHandlerDictionary> _mockHandlerDictionary = new();

        private readonly Mock<ITypeActivator> _mockTypeActivator = new();

        private readonly Mock<ILogger<HandlerFactory>> _mockLogger = new();

        #endregion

        #region Constructor

        public HandlerFactoryTests()
        {
            var mockLoggerFactory = new Mock<ILoggerFactory>();

            mockLoggerFactory.Setup(_ => _.NewLogger<HandlerFactory>()).Returns(() => _mockLogger.Object);

            _handlerFactory = new HandlerFactory(_mockTypeActivator.Object, _mockHandlerDictionary.Object, mockLoggerFactory.Object);
        }

        #endregion

        #region Handler Instantiated Tests

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
        public void HandlerFactory_Given_HandlerDictionaryReturnsHandler_ShouldLog_Trace()
        {
            var request = new ExampleRequest();
            var handlerType = typeof(ExampleRequestHandler);

            _mockHandlerDictionary.Setup(_ => _.GetHandlerType(request.GetType())).Returns(handlerType);

            _mockTypeActivator.Setup(_ => _.Instantiate<IBaseHandler>(handlerType)).Returns(new ExampleRequestHandler());

            _handlerFactory.NewHandler(new ExampleRequest());

            _mockLogger.Verify(_ => _.LogTrace(It.IsAny<LogMessage>(), null, "NewHandler"), Times.Once);
        }

        #endregion

        #region Handler Not Found Tests

        [Fact]
        public void HandlerFactory_Given_HandlerNotExisting_ShouldThrow_DoesNotExistException()
        {
            Assert.Throws<DoesNotExistException>(() => _handlerFactory.NewHandler(new ExampleRequest()));
        }

        [Fact]
        public void HandlerFactory_Given_HandlerNotExisting_ShouldLog_Critical()
        {
            var request = new ExampleRequest();

            Record.Exception(() => _handlerFactory.NewHandler(new ExampleRequest()));

            _mockLogger.Verify(_ => _.LogCritical(It.IsAny<LogMessage>(), null, "NewHandler"), Times.Once);
        }

        #endregion

        #region Request Is Null Tests

        [Fact]
        public void HandlerFactory_Given_Null_ShouldThrow_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _handlerFactory.NewHandler((object)null!));
        }

        [Fact]
        public void HandlerFactory_Given_Null_ShouldLog_Critical()
        {
            Record.Exception(() => _handlerFactory.NewHandler((object)null!));

            _mockLogger.Verify(_ => _.LogCritical(It.IsAny<LogMessage>(), null, "NewHandler"), Times.Once);
        }

        #endregion
    }
}
