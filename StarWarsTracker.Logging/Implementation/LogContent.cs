using StarWarsTracker.Domain.Enums;

namespace StarWarsTracker.Logging.Implementation
{
    public class LogContent
    {
        public LogContent(LogLevel logLevel, string className, string nameSpace, string methodCalling, string description, object? extra, double elapsedMilliseconds)
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

        public double ElapsedMilliseconds { get; set; }

        public DateTime DateTimeCreatedUTC { get; set; } = DateTime.UtcNow;
    }
}
