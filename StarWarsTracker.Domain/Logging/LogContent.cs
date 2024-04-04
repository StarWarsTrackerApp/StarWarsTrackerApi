using StarWarsTracker.Domain.Enums;

namespace StarWarsTracker.Domain.Logging
{
    public class LogContent
    {
        public LogContent(LogLevel logLevel, string className, string nameSpace, string methodCalling, string description, object? extra, long elapsedMilliseconds)
        {
            LogLevel = logLevel;
            NameOfLogLevel = logLevel.ToString();
            ClassName = className;
            NameSpace = nameSpace;
            MethodCalling = methodCalling;
            Description = description;
            Extra = extra;
            ElapsedMilliseconds = elapsedMilliseconds;
        }

        public LogLevel LogLevel { get; set; }

        public string NameOfLogLevel { get; set; }

        public string ClassName { get; set; }

        public string NameSpace { get; set; }

        public string MethodCalling { get; set; }

        public string Description { get; set; }

        public object? Extra { get; set; }

        public long ElapsedMilliseconds { get; set; }

        public DateTime DateTimeCreatedUTC { get; set; } = DateTime.UtcNow;
    }
}
