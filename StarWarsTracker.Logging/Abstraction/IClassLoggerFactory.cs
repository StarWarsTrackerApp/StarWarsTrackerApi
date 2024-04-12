namespace StarWarsTracker.Logging.Abstraction
{
    public interface IClassLoggerFactory
    {
        public IClassLogger GetLoggerFor<T>(T classToLogFor) where T : class;
    }
}
