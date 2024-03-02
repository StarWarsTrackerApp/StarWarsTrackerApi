using StarWarsTracker.Persistence.Abstraction;

namespace StarWarsTracker.Persistence.Tests.DataRequestTests
{
    public abstract class DataRequestTest
    {
        protected readonly IDataAccess _dataAccess = TestDataAccess.SharedInstance;
    }
}
