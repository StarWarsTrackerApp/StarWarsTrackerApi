using Moq;
using StarWarsTracker.Application.Abstraction;
using StarWarsTracker.Application.Implementation;
using StarWarsTracker.Application.Tests.ImplementationTests.TestRequests;
using StarWarsTracker.Domain.Enums;
using StarWarsTracker.Domain.Exceptions;
using StarWarsTracker.Logging.Abstraction;

namespace StarWarsTracker.Application.Tests.ImplementationTests
{
    public class HandlerFactoryTests
    {
        #region Private Members

        private readonly HandlerFactory _handlerFactory;

        private readonly Mock<IHandlerDictionary> _mockHandlerDictionary = new();

        private readonly Mock<ITypeActivator> _mockTypeActivator = new();

        private readonly Mock<IClassLogger> _mockLogger = new();

        private readonly Mock<IClassLoggerFactory> _mockLoggerFactory = new();

        #endregion

        #region Constructor

        public HandlerFactoryTests()
        {
            _mockLoggerFactory = new Mock<IClassLoggerFactory>();

            _mockLoggerFactory.Setup(_ => _.GetLoggerFor(It.IsAny<It.IsAnyType>())).Returns(() => _mockLogger.Object);

            _handlerFactory = new HandlerFactory(_mockTypeActivator.Object, _mockHandlerDictionary.Object, _mockLoggerFactory.Object);
        }

        #endregion

        #region Handler Instantiated Tests

        [Fact]
        public void HandlerFactory_Given_HandlerDictionaryReturnsHandler_ShouldReturn_HandlerInstantiatedByActivator()
        {
            var request = new ExampleRequest();
            var handlerType = typeof(ExampleRequestHandler);
            
            _mockHandlerDictionary.Setup(_ => _.GetHandlerType(request.GetType())).Returns(handlerType);

            _mockTypeActivator.Setup(_ => _.Instantiate<IBaseHandler>(handlerType)).Returns(new ExampleRequestHandler(_mockLoggerFactory.Object));

            var result = _handlerFactory.NewHandler(new ExampleRequest());

            Assert.NotNull(result);
        }

        [Fact]
        public void HandlerFactory_Given_HandlerDictionaryReturnsHandler_ShouldLog_Trace_Twice()
        {
            var request = new ExampleRequest();
            var handlerType = typeof(ExampleRequestHandler);

            _mockHandlerDictionary.Setup(_ => _.GetHandlerType(request.GetType())).Returns(handlerType);

            _mockTypeActivator.Setup(_ => _.Instantiate<IBaseHandler>(handlerType)).Returns(new ExampleRequestHandler(_mockLoggerFactory.Object));

            _handlerFactory.NewHandler(new ExampleRequest());

            _mockLogger.Verify(_ => _.AddTrace(It.IsAny<string>(), It.IsAny<object?>(), "NewHandler"), Times.Exactly(2));
        }

        [Fact]
        public void HandlerFactory_Given_HandlerDictionaryReturnsHandler_ShouldLog_Info_Twice()
        {
            var request = new ExampleRequest();
            var handlerType = typeof(ExampleRequestHandler);

            _mockHandlerDictionary.Setup(_ => _.GetHandlerType(request.GetType())).Returns(handlerType);

            _mockTypeActivator.Setup(_ => _.Instantiate<IBaseHandler>(handlerType)).Returns(new ExampleRequestHandler(_mockLoggerFactory.Object));

            _handlerFactory.NewHandler(new ExampleRequest());

            _mockLogger.Verify(_ => _.AddInfo(It.IsAny<string>(), It.IsAny<object?>(), "NewHandler"), Times.Exactly(2));
        }

        #endregion

        #region Handler Not Found Tests

        [Fact]
        public void HandlerFactory_Given_HandlerNotExisting_ShouldThrow_DoesNotExistException()
        {
            Assert.Throws<DoesNotExistException>(() => _handlerFactory.NewHandler(new ExampleRequest()));
        }

        [Fact]
        public void HandlerFactory_Given_HandlerNotExisting_ShouldIncreaseLogLevel_Critical()
        {
            var request = new ExampleRequest();

            Record.Exception(() => _handlerFactory.NewHandler(new ExampleRequest()));

            _mockLogger.Verify(_ => _.IncreaseLevel(LogLevel.Critical, It.IsAny<string>(), request.GetType().Name, "NewHandler"), Times.Once);
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

            _mockLogger.Verify(_ => _.IncreaseLevel(LogLevel.Critical, It.IsAny<string>(), null, "NewHandler"), Times.Once);
        }

        #endregion
    }
}
