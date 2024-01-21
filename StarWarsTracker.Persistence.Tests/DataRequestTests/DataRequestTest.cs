using StarWarsTracker.Persistence.Abstraction;
using StarWarsTracker.Persistence.Implementation;

namespace StarWarsTracker.Persistence.Tests.DataRequestTests
{
    public abstract class DataRequestTest
    {
        protected readonly IDataAccess _dataAccess;

        public DataRequestTest()
        {
            _dataAccess = new DataAccess(new SqlConnectionFactory(Hidden.DbServer, Hidden.DbName));
        }
    }
}
