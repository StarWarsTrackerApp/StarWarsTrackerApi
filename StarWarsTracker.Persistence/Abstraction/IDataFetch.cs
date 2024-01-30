namespace StarWarsTracker.Persistence.Abstraction
{
    /// <summary>
    /// This is the interface that will be implemented for any Sql Queries that will return a response object.
    /// The TResponse passed in should be the class which matches the DTO we expect to receive from the query.
    /// </summary>
    public interface IDataFetch<TResponse> : IDataRequest { }
}
