using Moq;
using StarWarsTracker.Domain.Logging;
using StarWarsTracker.Persistence.Abstraction;
using StarWarsTracker.Persistence.Implementation;
using System.Diagnostics.CodeAnalysis;

namespace StarWarsTracker.Tests.Shared.Helpers
{
    [ExcludeFromCodeCoverage]
    public static class TestDataAccess
    {        
        public static IDataAccess SharedInstance = 
            new DataAccess(new SqlConnectionFactory(Hidden.DbServer, Hidden.DbName), new Mock<ILogMessage>().Object, new Mock<ILogConfig>().Object);
    }
}
