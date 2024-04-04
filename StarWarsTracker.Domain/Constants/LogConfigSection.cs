namespace StarWarsTracker.Domain.Constants
{
    public class LogConfigSection
    {
        public const string Default = "Default";

        #region Logging Config Sections

        /// <summary>
        /// Database Logger Settings Will Determine What the Minimum LogLevel must be on a message to write it to the database, and what level of LogDetails to save from the LogMessage.
        /// </summary>
        public const string DatabaseLogger = "DatabaseLogger";

        /// <summary>
        /// SqlLogSettings will determine what logLevel to write the SqlLogs at, and what level of Detail to include in the logs.
        /// </summary>
        public const string SqlLogging = "SqlLogging";

        /// <summary>
        /// Exception Logging will determine what level to increase the LogMessage to and to write the Exception Logs at.
        /// </summary>
        public const string ExceptionLogging = "ExceptionLogging";

        /// <summary>
        /// Custom Log Levels that do not fall under the category of any other logging Config Section.
        /// </summary>
        public const string CustomLogLevels = "CustomLogLevels";

        #endregion
    }
}
