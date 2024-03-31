using StarWarsTracker.Domain.Enums;

namespace StarWarsTracker.Domain.Logging
{
    public class LogContent
    {
        public LogContent(LogLevel logLevel, string methodCalling, string description, object? extra)
        {
            LogLevel = logLevel;
            MethodCalling = methodCalling;
            Description = description;
            Extra = extra;
        }

        public string MethodCalling { get; set; }

        public string Description { get; set; }

        public object? Extra { get; set; }

        public LogLevel LogLevel { get; set; }

        public readonly DateTime DateTimeCreatedUTC = DateTime.UtcNow;
    }
}
