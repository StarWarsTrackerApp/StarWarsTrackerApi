namespace StarWarsTracker.Persistence.Abstraction
{
    /// <summary>
    /// This interface defines that every DataRequest must be able to GetSql() for the Sql Statement to execute, 
    /// and GetParameters() that the query will need. If no parameters are needed then a null object can be returned.
    /// </summary>
    public interface IDataRequest
    {
        public string GetSql();

        public object? GetParameters();
    }

    /// <summary>
    /// This is the interface that will be implemented for any Sql Commands such as Insert, Update, and Delete which do not return a response object.
    /// </summary>
    public interface IDataExecute : IDataRequest { }

    /// <summary>
    /// This is the interface that will be implemented for any Sql Queries that will return a response object.
    /// The TResponse passed in should be the class which matches the DTO we expect to receive from the query.
    /// </summary>
    public interface IDataFetch<TResponse> : IDataRequest where TResponse : class { }
}
