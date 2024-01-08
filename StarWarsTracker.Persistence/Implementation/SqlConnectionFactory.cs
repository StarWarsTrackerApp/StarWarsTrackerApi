using System.Data;
using System.Data.SqlClient;

namespace StarWarsTracker.Persistence.Implementation
{
    internal class SqlConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;

        public SqlConnectionFactory(string connectionString) => _connectionString = connectionString;

        public SqlConnectionFactory(string server, string databaseName, bool useIntegratedSecurity = true, string ? userId = null, string? password = null)
        {
            var builder = new SqlConnectionStringBuilder()
            {
                DataSource = server,
                InitialCatalog = databaseName,
                IntegratedSecurity = useIntegratedSecurity
            };

            if (!useIntegratedSecurity)
            {
                builder.UserID = userId;
                builder.Password = password;
            }

            _connectionString = builder.ConnectionString;
        }

        public IDbConnection NewConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
