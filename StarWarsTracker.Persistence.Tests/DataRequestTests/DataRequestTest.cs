using StarWarsTracker.Persistence.Abstraction;
using StarWarsTracker.Persistence.Implementation;

namespace StarWarsTracker.Persistence.Tests.DataRequestTests
{
    public abstract class DataRequestTest
    {
        protected readonly IDataAccess _dataAccess;

        public DataRequestTest()
        {
            /*
                Hidden.cs class has been added to Git Ignore - You will need to add your own Hidden.cs to the TicketMate.Persistence.Tests project
                and add the properties for Hidden.cs since this class is not tracked in Git Source Control.
                
                Example:    
                            internal class Hidden
                            {
                                internal const string DbServer = "";
                                internal const string DbName = "";
                                internal const string DbUserId = "";
                                internal const string DbPassword = "";
                            }
            */

            _dataAccess = new DataAccess(new SqlConnectionFactory(Hidden.DbServer, Hidden.DbName));

        }
    }
}
