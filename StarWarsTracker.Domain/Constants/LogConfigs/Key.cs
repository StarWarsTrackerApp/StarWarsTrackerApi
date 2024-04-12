namespace StarWarsTracker.Domain.Constants.LogConfigs
{
    public class Key
    {

        #region Database Log Settings

        public const string LogMessageLevelToWrite = "LogMessageLevelToWrite";
        
        public const string LogContentLevelToWrite = "LogContentLevelToWrite";

        #endregion

        #region Sql Execute/Fetch/FetchList Configs

        public const string SqlRequestLogLevel = "SqlRequestLogLevel";

        public const string SqlRequestLogDetails = "SqlRequestLogDetails";

        public const string SqlResponseLogLevel = "SqlResponseLogLevel";

        public const string SqlResponseLogDetails = "SqlResponseLogDetails";

        #endregion

        #region Exception Logging Configs

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

        #region Controller Logging

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
