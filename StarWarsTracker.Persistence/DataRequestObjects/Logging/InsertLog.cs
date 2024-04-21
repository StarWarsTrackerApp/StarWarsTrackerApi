namespace StarWarsTracker.Persistence.DataRequestObjects.Logging
{
    public class InsertLog : IDataExecute
    {
        #region Constructor

        public InsertLog(int logLevel, string message, string className, string methodName, string? stackTrace)
        {
            LogLevel = logLevel;
            Message = message;
            ClassName = className;
            MethodName = methodName;
            StackTrace = stackTrace;
        }

        #endregion

        #region Public Properties / SQL Parameters

        public int LogLevel { get; set; }

        public string Message { get; set; }

        public string ClassName { get; set; }

        public string MethodName { get; set; }

        public string? StackTrace {  get; set; }

        #endregion

        #region Public IDataExecute Methods

        public object? GetParameters() => this;

        public string GetSql() => 
        @$"
            INSERT INTO {TableName.Log} ( LogLevelId, Message, ClassName, MethodName, StackTrace ) 
                                 VALUES ( @LogLevel, @Message, @ClassName, @MethodName, @StackTrace )
        ";

        #endregion
    }
}
