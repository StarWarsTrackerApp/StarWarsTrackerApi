namespace StarWarsTracker.Persistence.Abstraction
{
    /// <summary>
    /// This interface defines that every DataRequest must be able to GetSql() for the Sql Statement to execute, 
    /// and GetParameters() that the query will need. If no parameters are needed then a null object can be returned.
    /// </summary>
    public interface IDataRequest
    {
        /// <summary>
        /// Get the SQL Query/Command for the DataRequest.
        /// </summary>
        /// <returns>string value of the SQL Query/Command</returns>
        public string GetSql();

        /// <summary>
        /// Returns Nullable object representing the Parameters used in the DataRequest's SQL Query/Command.
        /// </summary>
        /// <returns>object representing the SQL Parameters. Null if no parameters.</returns>
        public object? GetParameters();
    }
}
