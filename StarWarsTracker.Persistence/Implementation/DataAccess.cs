using Dapper;
using StarWarsTracker.Domain.Enums;
using StarWarsTracker.Domain.Logging;

namespace StarWarsTracker.Persistence.Implementation
{
    internal class DataAccess : IDataAccess
    {
        #region Private Members

        private readonly IDbConnectionFactory _connectionFactory;

        private readonly ILogMessage _logMessage;

        private readonly ILogConfig _logConfig;

        #endregion

        #region Constructor 

        public DataAccess(IDbConnectionFactory connectionFactory, ILogMessage logMessage, ILogConfig logConfig)
        {
            _connectionFactory = connectionFactory;

            _logMessage = logMessage;

            _logConfig = logConfig;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Send IDataExecute Requests through Dapper to use connection.ExecuteAsync for any Insert, Update, or Delete SQL Command.
        /// </summary>
        /// <param name="request">The IDataExecute Request to send to Dapper which contains the SQL and the Parameters that will be used.</param>
        /// <returns>Returns the number of rows that are affected by the SQL Command that is executed.</returns>
        public async Task<int> ExecuteAsync(IDataExecute request)
        {
            _logMessage.AddConfiguredLogLevel(LogConfigSection.SqlLogging, LogConfigKey.ExecuteSqlRequestLogLevel, this, "Executing SQL Request", 
                GetRequestToLog(request, LogConfigKey.ExecuteSqlRequestLogDetails));

            using var connection = _connectionFactory.NewConnection();

            _logMessage.AddTrace(this, "Connection Created", connection.ConnectionString);

            connection.Open();

            _logMessage.AddTrace(this, "Opened Connection");

            var response = await connection.ExecuteAsync(request.GetSql(), request.GetParameters());

            _logMessage.AddConfiguredLogLevel(LogConfigSection.SqlLogging, LogConfigKey.ExecuteSqlResponseLogLevel, this, "Rows Affected", 
                GetResponseToLog(response, LogConfigKey.ExecuteSqlResponseLogDetails));

            return response;
        }

        /// <summary>
        /// Sends IDataFetch Requests through Dapper to use connection.QueryFirstOrDefaultAsync for any SQL Query.
        /// </summary>
        /// <typeparam name="TResponse">The DTO that will be fetched from the Database using the IDataFetch Request</typeparam>
        /// <param name="request">The IDataFetch Request to send to Dapper which contains the SQL and the Parameters that will be used.</param>
        /// <returns>Returns the first record found or default if none found.</returns>
        public async Task<TResponse?> FetchAsync<TResponse>(IDataFetch<TResponse> request)
        {
            _logMessage.AddConfiguredLogLevel(LogConfigSection.SqlLogging, LogConfigKey.FetchSqlRequestLogLevel, this, "Fetching SQL Request", 
                GetRequestToLog(request, LogConfigKey.FetchSqlRequestLogDetails));

            using var connection = _connectionFactory.NewConnection();

            _logMessage.AddTrace(this, "Database Connection Created", connection.ConnectionString);

            connection.Open();

            _logMessage.AddTrace(this, "Database Connection Opened");

            var response = await connection.QueryFirstOrDefaultAsync<TResponse>(request.GetSql(), request.GetParameters());

            _logMessage.AddConfiguredLogLevel(LogConfigSection.SqlLogging, LogConfigKey.FetchSqlResponseLogLevel, this, "Response Fetched", 
                GetResponseToLog<TResponse>(response, LogConfigKey.FetchSqlResponseLogDetails));

            return response;
        }

        /// <summary>
        /// Sends IDataFetch Requests through Dapper to use connection.QueryAsync for any SQL Query.
        /// </summary>
        /// <typeparam name="TResponse">The DTO that will be fetched from the Database using the IDataFetch Request</typeparam>
        /// <param name="request">The IDataFetch Request to send to Dapper which contains the SQL and the Parameters that will be used.</param>
        /// <returns>Returns the records found or an empty collection if none found.</returns>
        public async Task<IEnumerable<TResponse>> FetchListAsync<TResponse>(IDataFetch<TResponse> request)
        {
            _logMessage.AddConfiguredLogLevel(LogConfigSection.SqlLogging, LogConfigKey.FetchListSqlRequestLogLevel, this, "Fetching SQL List Request", 
                GetRequestToLog(request, LogConfigKey.FetchListSqlRequestLogDetails));

            using var connection = _connectionFactory.NewConnection();

            _logMessage.AddTrace(this, "Connection Created", connection.ConnectionString);

            connection.Open();

            _logMessage.AddTrace(this, "Opened Connection");

            var response = await connection.QueryAsync<TResponse>(request.GetSql(), request.GetParameters());

            _logMessage.AddConfiguredLogLevel(LogConfigSection.SqlLogging, LogConfigKey.FetchListSqlResponseLogLevel, this, "Response Fetched", 
                GetResponseToLog(response, LogConfigKey.FetchListSqlResponseLogDetails));

            return response;
        }

        #endregion

        #region Private Methods For Logging

        private object? GetRequestToLog<T>(T request, string logDetailsConfigKey) where T : IDataRequest =>
            _logConfig.GetLogLevel(LogConfigSection.SqlLogging, logDetailsConfigKey) switch
            {
                LogLevel.Trace => 
                    new { request.GetType().Name, Parameters = request.GetParameters(), Sql = request.GetSql() },
                
                LogLevel.Debug => 
                    new { request.GetType().Name, Parameters = request.GetParameters() },
                
                LogLevel.Information or LogLevel.Warning or LogLevel.Error or LogLevel.Critical => 
                new { request.GetType().Name },
                
                _ => null,
            };

        private object? GetResponseToLog<T>(T? response, string logDetailsConfigKey) =>
            _logConfig.GetLogLevel(LogConfigSection.SqlLogging, logDetailsConfigKey) switch
            {
                LogLevel.Trace => 
                    new { response?.GetType().Name, response },

                LogLevel.Debug or LogLevel.Information or LogLevel.Warning or LogLevel.Error or LogLevel.Critical => 
                    new { response?.GetType().Name, response },
                
                _ => null,
            };

        #endregion
    }
}
