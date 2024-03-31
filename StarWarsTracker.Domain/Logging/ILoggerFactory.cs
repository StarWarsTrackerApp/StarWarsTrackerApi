namespace StarWarsTracker.Domain.Logging
{
    public interface ILoggerFactory
    {
        public ILogger<T> NewLogger<T>();
    }
}
