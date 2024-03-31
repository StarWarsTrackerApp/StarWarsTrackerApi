namespace StarWarsTracker.Persistence.DataRequestObjects.Logging
{
    public class InsertLog : IDataExecute
    {
        public InsertLog(int logLevel, string message, string className, string methodName, string? stackTrace)
        {
            LogLevel = logLevel;
            Message = message;
            ClassName = className;
            MethodName = methodName;
            StackTrace = stackTrace;
        }

        public int LogLevel { get; set; }

        public string Message { get; set; }

        public string ClassName { get; set; }

        public string MethodName { get; set; }

        public string? StackTrace {  get; set; }

        public object? GetParameters() => this;

        public string GetSql() => $"INSERT INTO {TableName.Log} (LogLevelId, Message, ClassName, MethodName, StackTrace) VALUES (@LogLevel, @Message, @ClassName, @MethodName, @StackTrace)";
    }
}
