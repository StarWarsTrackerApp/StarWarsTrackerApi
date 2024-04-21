using Moq;
using StarWarsTracker.Domain.Enums;
using StarWarsTracker.Domain.Models;
using StarWarsTracker.Logging.Abstraction;
using StarWarsTracker.Logging.Implementation;

namespace StarWarsTracker.Logging.Tests.ImplementationTests
{
    public class ClassLoggerFactoryTests
    {
        #region Private Members

        private readonly ClassLoggerFactory _factory;

        private readonly Mock<ILogMessage> _mockLogMessage = new();

        #endregion

        #region Constructor

        public ClassLoggerFactoryTests()
        {
            _factory = new(_mockLogMessage.Object, new Mock<ILogConfigReader>().Object);
        }

        #endregion

        #region GetLoggerFor Test

        [Fact]
        public void GetLoggerFor_Given_Object_ShouldReturn_ClassLogger_ForThatObject()
        {
            var obj = new Event();

            var description = "Description";
            var extra = "Extra";
            var methodCalling = "MethodCalling";
            var logLevel = LogLevel.Critical;

            var result = _factory.GetLoggerFor(obj);

            Assert.NotNull(result);

            result.IncreaseLevel(logLevel, description, extra, methodCalling);

            _mockLogMessage.Verify(_ => 
                //_.IncreaseLevel(It.IsAny<LogContent>())
                _.IncreaseLevel(It.Is<LogContent>(_ => 
                       _.LogLevel == logLevel 
                    && _.Description == description
                    && _.Extra as string == extra
                    && _.MethodCalling == methodCalling
                    && _.ClassName == obj.GetType().Name
                    && _.NameSpace == obj.GetType().Namespace
                )), Times.Once());
        }
        
        #endregion
    }
}
