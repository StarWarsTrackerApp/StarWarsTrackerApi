using StarWarsTracker.Domain.Enums;
using StarWarsTracker.Logging.Implementation;

namespace StarWarsTracker.Logging.Abstraction
{
    public interface ILogMessage
    {
        public void AddContent(LogContent logContent);

        public void IncreaseLevel(LogContent logContent);

        public LogLevel GetLevel();

        public string GetMessageJson(LogLevel logLevel);

        public IEnumerable<LogContent> GetContent(LogLevel logLevel);

        public double GetElapsedMilliseconds();
    }
}
