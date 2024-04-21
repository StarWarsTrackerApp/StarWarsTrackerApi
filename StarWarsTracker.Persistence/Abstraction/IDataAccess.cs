namespace StarWarsTracker.Persistence.Abstraction
{
    /// <summary>
    /// This interface will abstract out the integration with Dapper. 
    /// Each method will receive either an IDataExecute or IDataFetch request, which will define the Sql and Parameters to pass to Dapper.
    /// </summary>
    public interface IDataAccess
    {
        /// <summary>
        /// Executes the DataRequest using GetSql() and GetParameters() from the IDataExecute request. 
        /// </summary>
        /// <param name="request">The type of DataRequest to Execute</param>
        /// <returns>The number of rows impacted</returns>
        public Task<int> ExecuteAsync(IDataExecute request);

        /// <summary>
        /// Fetch the FirstOrDefault result from the query provided by the DataRequest using GetSql() and GetParameters() from the IDataFetch.
        /// </summary>
        /// <typeparam name="TResponse">The type of DTO being returned by the SQL Query</typeparam>
        /// <param name="request">The type of DataRequest to Fetch</param>
        /// <returns>Returns an object of the type TResponse associated with the IDataFetch.</returns>
        public Task<TResponse?> FetchAsync<TResponse>(IDataFetch<TResponse> request);

        /// <summary>
        /// Fetch the collection of result from the query provided by the DataRequest using GetSql() and GetParameters() from the IDataFetch.
        /// </summary>
        /// <typeparam name="TResponse">The type of DTO that a collection of is being returned by the SQL Query</typeparam>
        /// <param name="request">The type of DataRequest to Fetch</param>
        /// <returns>Returns an IEnumerable of objects of the type TResponse associated with the IDataFetch.</returns>
        public Task<IEnumerable<TResponse>> FetchListAsync<TResponse>(IDataFetch<TResponse> request);
    }
}
