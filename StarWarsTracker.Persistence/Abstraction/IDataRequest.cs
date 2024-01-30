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
}
