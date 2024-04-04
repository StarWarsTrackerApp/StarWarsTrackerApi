namespace StarWarsTracker.Domain.Logging
{
    public interface ILogger
    {
        public void Log(ILogMessage message, string route, string method);
    }
}
