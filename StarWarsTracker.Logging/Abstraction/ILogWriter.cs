namespace StarWarsTracker.Logging.Abstraction
{
    public interface ILogWriter
    {
        public void Write(ILogMessage logMessage, string requestPath, string httpMethod);
    }
}
