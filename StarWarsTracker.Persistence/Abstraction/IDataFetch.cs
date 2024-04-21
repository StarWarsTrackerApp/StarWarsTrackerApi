namespace StarWarsTracker.Persistence.Abstraction
{
    /// <summary>
    /// This is the interface that will be implemented for any Sql Queries that will return a response object.
    /// </summary>
    /// <typeparam name="TResponse">The type of DTO being returned by the SQL Query</typeparam>
    public interface IDataFetch<TResponse> : IDataRequest { }
}
