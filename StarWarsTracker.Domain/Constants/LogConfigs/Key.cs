namespace StarWarsTracker.Domain.Constants.LogConfigs
{
    /// <summary>
    /// The constants belonging to this class are related to specific LogLevel Configurations.
    /// </summary>
    public class Key
    {
        #region Database Log Settings

        /// <summary>
        /// Configures what LogLevel a LogMessage must be equal or greater than in order to be written to the Database.
        /// </summary>
        public const string LogMessageLevelToWrite = "LogMessageLevelToWrite";
        
        /// <summary>
        /// Configures what LogLevel a LogContent must be equal or greater than in order to be included in the LogMessage written to the Database.
        /// </summary>
        public const string LogContentLevelToWrite = "LogContentLevelToWrite";

        #endregion

        #region Sql Execute/Fetch/FetchList Configs

        /// <summary>
        /// Configures what level the LogContent will be logged as when logging Sql Requests.
        /// </summary>
        public const string SqlRequestLogLevel = "SqlRequestLogLevel";

        /// <summary>
        /// Configures what level of detail the LogContent will include when logging Sql Requests.
        /// </summary>
        public const string SqlRequestLogDetails = "SqlRequestLogDetails";

        /// <summary>
        /// Configures what level the LogContent will be logged as when logging Sql Responses.
        /// </summary>
        public const string SqlResponseLogLevel = "SqlResponseLogLevel";

        /// <summary>
        /// Configures what level of detail the LogContent will include when logging Sql Responses.
        /// </summary>
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
