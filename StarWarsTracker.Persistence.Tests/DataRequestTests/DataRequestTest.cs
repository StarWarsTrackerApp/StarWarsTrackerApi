using StarWarsTracker.Persistence.Abstraction;
using StarWarsTracker.Tests.Shared.Helpers;

namespace StarWarsTracker.Persistence.Tests.DataRequestTests
{
    public abstract class DataRequestTest
    {
        protected readonly IDataAccess _dataAccess = TestDataAccess.SharedInstance;
    }
}
