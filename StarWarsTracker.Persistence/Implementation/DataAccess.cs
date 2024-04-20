using Dapper;
using StarWarsTracker.Domain.Constants.LogConfigs;
using StarWarsTracker.Domain.Enums;
using StarWarsTracker.Logging.Abstraction;
using System.Runtime.CompilerServices;

namespace StarWarsTracker.Persistence.Implementation
{
    internal class DataAccess : IDataAccess
    {
        #region Private Members

        private readonly IDbConnectionFactory _connectionFactory;

        private readonly IClassLogger _logger;

        private readonly ILogConfigReader _logConfigReader;

        #endregion

        #region Constructor 

        public DataAccess(IDbConnectionFactory connectionFactory, IClassLoggerFactory loggerFactory, ILogConfigReader logConfigReader)
        {
            _connectionFactory = connectionFactory;

            _logger = loggerFactory.GetLoggerFor(this);

            _logConfigReader = logConfigReader;
        }

        #endregion

        #region Public Methods

        public async Task<int> ExecuteAsync(IDataExecute request)
        {
            LogRequest(Section.SqlExecute, request);

            using var connection = _connectionFactory.NewConnection();

            _logger.AddTrace("Connection Created", connection.ConnectionString);

            connection.Open();

            _logger.AddTrace("Opened Connection");

            var response = await connection.ExecuteAsync(request.GetSql(), request.GetParameters());

            LogResponse(Section.SqlExecute, response);
            
            return response;
        }

        public async Task<TResponse?> FetchAsync<TResponse>(IDataFetch<TResponse> request)
        {
            LogRequest(Section.SqlFetch, request);

            using var connection = _connectionFactory.NewConnection();

            _logger.AddTrace("Database Connection Created", connection.ConnectionString);

            connection.Open();

            _logger.AddTrace("Database Connection Opened");

            var response = await connection.QueryFirstOrDefaultAsync<TResponse>(request.GetSql(), request.GetParameters());

            LogResponse(Section.SqlFetch, response);

            return response;
        }

        public async Task<IEnumerable<TResponse>> FetchListAsync<TResponse>(IDataFetch<TResponse> request)
        {
            LogRequest(Section.SqlFetchList, request);

            using var connection = _connectionFactory.NewConnection();

            _logger.AddTrace("Connection Created", connection.ConnectionString);

            connection.Open();

            _logger.AddTrace("Opened Connection");

            var response = await connection.QueryAsync<TResponse>(request.GetSql(), request.GetParameters());

            LogResponse(Section.SqlFetchList, response);

            return response;
        }

        #endregion

        #region Private Methods For Logging

        /// <summary>
        /// Helper method for Logging the DataRequest
        /// </summary>
        private void LogRequest<T>(string logConfigSection, T request, [CallerMemberName] string methodCalling = "") where T : IDataRequest
        {
            var logDetailLevel = _logConfigReader.GetCustomLogLevel(logConfigSection, Key.SqlRequestLogDetails);

            object? extra = logDetailLevel switch
            {
                LogLevel.Trace =>
                    new { request.GetType().Name, Parameters = request.GetParameters(), Sql = request.GetSql() },

                LogLevel.Debug =>
                    new { request.GetType().Name, Parameters = request.GetParameters() },

                LogLevel.Information or LogLevel.Warning or LogLevel.Error or LogLevel.Critical =>
                new { request.GetType().Name },

                _ => null,
            };

            _logger.AddConfiguredLogLevel(logConfigSection, Key.SqlRequestLogLevel, "Sql Request Received", extra, methodCalling);
        }

        /// <summary>
        /// Helper method for Logging the DataRequest's Response
        /// </summary>
        private void LogResponse<T>(string logConfigSection, T? response, [CallerMemberName] string methodCalling = "")
        {
            var logDetailLevel = _logConfigReader.GetCustomLogLevel(logConfigSection, Key.SqlResponseLogDetails);

            object? extra = logDetailLevel switch
            {
                LogLevel.Trace =>
                    new { response?.GetType().Name, response },

                LogLevel.Debug or LogLevel.Information or LogLevel.Warning or LogLevel.Error or LogLevel.Critical =>
                    new { response?.GetType().Name, response },

                _ => null
            };

            _logger.AddConfiguredLogLevel(logConfigSection, Key.SqlResponseLogLevel, "Sql Response Received", extra, methodCalling);
        }
       
        #endregion
    }
}
