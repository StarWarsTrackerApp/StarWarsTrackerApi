using Dapper;

namespace StarWarsTracker.Persistence.Implementation
{
    internal class DataAccess : IDataAccess
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public DataAccess(IDbConnectionFactory connectionFactory) => _connectionFactory = connectionFactory;

        public async Task<int> ExecuteAsync(IDataExecute request)
        {
            using var connection = _connectionFactory.NewConnection();

            connection.Open();

            return await connection.ExecuteAsync(request.GetSql(), request.GetParameters());
        }

        public async Task<TResponse?> FetchAsync<TResponse>(IDataFetch<TResponse> request) where TResponse : class
        {
            using var connection = _connectionFactory.NewConnection();

            connection.Open();

            return await connection.QueryFirstOrDefaultAsync<TResponse>(request.GetSql(), request.GetParameters());
        }

        public async Task<IEnumerable<TResponse>> FetchListAsync<TResponse>(IDataFetch<TResponse> request) where TResponse : class
        {
            using var connection = _connectionFactory.NewConnection();

            connection.Open();

            return await connection.QueryAsync<TResponse>(request.GetSql(), request.GetParameters());
        }
    }
}
