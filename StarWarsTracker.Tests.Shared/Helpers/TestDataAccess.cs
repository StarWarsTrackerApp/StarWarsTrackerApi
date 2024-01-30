using StarWarsTracker.Persistence.Abstraction;
using StarWarsTracker.Persistence.Implementation;

namespace StarWarsTracker.Tests.Shared.Helpers
{
    public static class TestDataAccess
    {
        public static IDataAccess SharedInstance = new DataAccess(new SqlConnectionFactory(Hidden.DbServer, Hidden.DbName));
    }
}
