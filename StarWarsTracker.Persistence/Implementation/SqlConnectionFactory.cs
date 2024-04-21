using System.Data;
using System.Data.SqlClient;

namespace StarWarsTracker.Persistence.Implementation
{
    /// <summary>
    /// This class implements IDbConnectionFactory in order to encapsulate the SqlConnection that is created and returned as an IDbConnection using this Factory class.
    /// </summary>
    internal class SqlConnectionFactory : IDbConnectionFactory
    {
        #region Private Members

        private readonly string _connectionString;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a SQL Connection Factory using the ConnectionString passed in.
        /// </summary>
        /// <param name="connectionString">ConnectionString to be used to connect to the database.</param>
        public SqlConnectionFactory(string connectionString) => _connectionString = connectionString;

        /// <summary>
        /// Crease a SQL Connection Factory using the server, databaseName, optional userId/password, and/or useIntegratedSecurity options.
        /// ConnectionString is build using SqlConnectionStringBuilder.
        /// </summary>
        /// <param name="server">The DatabaseServer to use in ConnectionString</param>
        /// <param name="databaseName">The DatabaseName to use in ConnectionString</param>
        /// <param name="useIntegratedSecurity">Defaults to true, specify if the ConnectionString should use integratedSecurity</param>
        /// <param name="userId">Defaults to null, specify if a userId should be used in ConnectionString</param>
        /// <param name="password">Defaults to null, specify if a password should be used in ConnectionString</param>
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

        #endregion

        #region Public IDbConnectionFactory Method

        public IDbConnection NewConnection()
        {
            return new SqlConnection(_connectionString);
        }

        #endregion
    }
}
