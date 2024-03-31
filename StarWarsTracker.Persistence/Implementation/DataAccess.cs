using Dapper;

namespace StarWarsTracker.Persistence.Implementation
{
    internal class DataAccess : IDataAccess
    {
        #region Private Members

        private readonly IDbConnectionFactory _connectionFactory;

        #endregion

        #region Constructor 

        public DataAccess(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
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
            using var connection = _connectionFactory.NewConnection();

            connection.Open();

            return await connection.ExecuteAsync(request.GetSql(), request.GetParameters());
        }

        /// <summary>
        /// Sends IDataFetch Requests through Dapper to use connection.QueryFirstOrDefaultAsync for any SQL Query.
        /// </summary>
        /// <typeparam name="TResponse">The DTO that will be fetched from the Database using the IDataFetch Request</typeparam>
        /// <param name="request">The IDataFetch Request to send to Dapper which contains the SQL and the Parameters that will be used.</param>
        /// <returns>Returns the first record found or default if none found.</returns>
        public async Task<TResponse?> FetchAsync<TResponse>(IDataFetch<TResponse> request)
        {
            using var connection = _connectionFactory.NewConnection();

            connection.Open();

            return await connection.QueryFirstOrDefaultAsync<TResponse>(request.GetSql(), request.GetParameters());
        }

        /// <summary>
        /// Sends IDataFetch Requests through Dapper to use connection.QueryAsync for any SQL Query.
        /// </summary>
        /// <typeparam name="TResponse">The DTO that will be fetched from the Database using the IDataFetch Request</typeparam>
        /// <param name="request">The IDataFetch Request to send to Dapper which contains the SQL and the Parameters that will be used.</param>
        /// <returns>Returns the records found or an empty collection if none found.</returns>
        public async Task<IEnumerable<TResponse>> FetchListAsync<TResponse>(IDataFetch<TResponse> request)
        {
            using var connection = _connectionFactory.NewConnection();

            connection.Open();

            return await connection.QueryAsync<TResponse>(request.GetSql(), request.GetParameters());
        }

        #endregion
    }
}
