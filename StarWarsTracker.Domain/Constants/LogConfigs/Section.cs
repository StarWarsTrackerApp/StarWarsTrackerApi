namespace StarWarsTracker.Domain.Constants.LogConfigs
{
    /// <summary>
    /// The Constants belonging to this class are related to LogConfig Sections from the appsettings.
    /// </summary>
    public class Section
    {
        /// <summary>
        /// This LogConfig Section is related to what Level of LogMessages to write to the database and what level of content to write.
        /// </summary>
        public const string DatabaseLogSettings = "DatabaseLogSettings";

        /// <summary>
        /// This LogConfig Section is related to what LogLevel and Detail Level to write for Sql Execute Request/Response Logging
        /// </summary>
        public const string SqlExecute = "SqlExecute";

        /// <summary>
        /// This LogConfig Section is related to what LogLevel and Detail Level to write for Sql Fetch Request/Response Logging
        /// </summary>
        public const string SqlFetch = "SqlFetch";

        /// <summary>
        /// This LogConfig Section is related to what LogLevel and Detail Level to write for Sql FetchList Request/Response Logging
        /// </summary>
        public const string SqlFetchList = "SqlFetchList";

        /// <summary>
        /// This LogConfig Section is related to what LogLevel to increase the LogMessage to when Exceptions are caught by the ExceptionHandlingMiddleware.
        /// </summary>
        public const string ExceptionLogging = "ExceptionLogging";

        /// <summary>
        /// This LogConfig Section is related to what LogLevel to write the RequestBody/Response for requests coming in/out of the API Controllers.
        /// </summary>
        public const string ControllerLogging = "ControllerLogging";
    }
}
