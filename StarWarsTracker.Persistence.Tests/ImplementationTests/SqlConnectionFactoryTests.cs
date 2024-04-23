using StarWarsTracker.Persistence.Implementation;
using StarWarsTracker.Tests.Shared;
using System.Data;

namespace StarWarsTracker.Persistence.Tests.ImplementationTests
{
    public class SqlConnectionFactoryTests
    {
        [Fact]
        public void SqlConnectionFactory_Given_DbServerDbNameIntegratedSecurity_ShouldReturn_ConnectionThatCanOpen()
        {
            var connectionFactory = new SqlConnectionFactory(Hidden.DbServer, Hidden.DbName);

            using var connection = connectionFactory.NewConnection();

            connection.Open();

            Assert.Equal(ConnectionState.Open, connection.State);
        }

        [Fact]
        public void SqlConnectionFactory_Given_ConnectionString_ShouldReturn_ConnectionThatCanOpen()
        {
            var connectionString = $"Data Source={Hidden.DbServer};Initial Catalog={Hidden.DbName};Integrated Security=True;";

            var connectionFactory = new SqlConnectionFactory(connectionString);

            using var connection = connectionFactory.NewConnection();

            connection.Open();

            Assert.Equal(ConnectionState.Open, connection.State);
        }

        [Fact]
        public void SqlConnectionFactory_Given_UsernameAndPassword_ReturnsConnection_WithExpectedConnectionString()
        {
            var dbServer = "server";
            var dbName = "dbName";
            var username = "username";
            var password = "password";
            var expectedConnectionString = $"Data Source={dbServer};Initial Catalog={dbName};Integrated Security=False;User ID={username};Password={password}";

            var connectionFactory = new SqlConnectionFactory(dbServer, dbName, useIntegratedSecurity: false, username, password);

            using var connection = connectionFactory.NewConnection();

            Assert.Equal(expectedConnectionString, connection.ConnectionString);
        }
    }
}
