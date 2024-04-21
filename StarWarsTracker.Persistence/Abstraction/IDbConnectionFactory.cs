namespace StarWarsTracker.Persistence.Abstraction
{
    /// <summary>
    /// This interface defines the factory that will return the IDbConnection used to connect to the database.
    /// </summary>
    public interface IDbConnectionFactory
    {
        public System.Data.IDbConnection NewConnection();
    }
}
