using Moq;
using StarWarsTracker.Logging.Abstraction;
using StarWarsTracker.Persistence.Abstraction;
using StarWarsTracker.Persistence.Implementation;
using System.Diagnostics.CodeAnalysis;

namespace StarWarsTracker.Tests.Shared.Helpers
{
    [ExcludeFromCodeCoverage]
    public static class TestDataAccess
    {        
        static TestDataAccess()
        {
            var mockClassLoggerFactory = new Mock<IClassLoggerFactory>();

            mockClassLoggerFactory.Setup(_ => _.GetLoggerFor(It.IsAny<It.IsAnyType>())).Returns(() => new Mock<IClassLogger>().Object);

            SharedInstance = new DataAccess(new SqlConnectionFactory(Hidden.DbServer, Hidden.DbName), mockClassLoggerFactory.Object, new Mock<ILogConfigReader>().Object);
        }

        public static readonly IDataAccess SharedInstance;
    }
}
