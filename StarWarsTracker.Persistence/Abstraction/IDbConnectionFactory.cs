namespace StarWarsTracker.Persistence.Abstraction
{
    public interface IDbConnectionFactory
    {
        public System.Data.IDbConnection NewConnection();
    }
}
