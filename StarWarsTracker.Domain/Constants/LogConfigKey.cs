namespace StarWarsTracker.Domain.Constants
{
    public static class LogConfigKey
    {
        #region Logging Settings

        /// <summary>
        /// What LogLevel to write logs at.
        /// </summary>
        public const string LogLevel = "LogLevel";

        /// <summary>
        /// The level of details to include in logs.
        /// </summary>
        public const string LogDetails = "LogDetails";

        #endregion

        #region Sql Logging Settings

        public const string ExecuteSqlRequestLogLevel = "ExecuteSqlRequestLogLevel";
        public const string ExecuteSqlRequestLogDetails = "ExecuteSqlRequestLogDetails";
        public const string ExecuteSqlResponseLogLevel = "ExecuteSqlResponseLogLevel";
        public const string ExecuteSqlResponseLogDetails = "ExecuteSqlResponseLogDetails";

        public const string FetchSqlRequestLogLevel = "FetchSqlRequestLogLevel";
        public const string FetchSqlRequestLogDetails = "FetchSqlRequestLogDetails";
        public const string FetchSqlResponseLogLevel = "FetchSqlResponseLogLevel";
        public const string FetchSqlResponseLogDetails = "FetchSqlResponseLogDetails";

        public const string FetchListSqlRequestLogLevel = "FetchListSqlRequestLogLevel";
        public const string FetchListSqlRequestLogDetails = "FetchListSqlRequestLogDetails";
        public const string FetchListSqlResponseLogLevel = "FetchListSqlResponseLogLevel";
        public const string FetchListSqlResponseLogDetails = "FetchListSqlResponseLogDetails";

        #endregion

        #region Exception LogMessage LogLevel Configs 

        /// <summary>
        /// What level to assign logs related to Exceptions caught by the Exception Handling Middleware.
        /// </summary>
        public const string DefaultExceptionLogLevel = "DefaultExceptionLogLevel";

        /// <summary>
        /// What level to assign logs related to DoesNotExistExceptions caught by the Exception Handing Middleware
        /// </summary>
        public const string DoesNotExistExceptionLogLevel = "DoesNotExistExceptionLogLevel";

        /// <summary>
        /// What level to assign logs related to AlreadyExistsException caught by the Exception Handing Middleware
        /// </summary>
        public const string AlreadyExistsExceptionLogLevel = "AlreadyExistsExceptionLogLevel";

        /// <summary>
        /// What level to assign logs related to ValidationFailureException caught by the Exception Handing Middleware
        /// </summary>
        public const string ValidationFailureExceptionLogLevel = "ValidationFailureExceptionLogLevel";

        #endregion

        #region Custom LogLevel Configs

        /// <summary>
        /// What level to assign LogContent that records the Request Body in Controller Layer.
        /// </summary>
        public const string ControllerRequestBodyLogLevel = "ControllerRequestBodyLogLevel";
        
        /// <summary>
        /// What level to assign the LogContent that records the Response Body in Controller Layer.
        /// </summary>
        public const string ControllerResponseBodyLogLevel = "ControllerResponseBodyLogLevel";        
            
        #endregion
    }
}
