namespace StarWarsTracker.Domain.Exceptions
{
    /// <summary>
    /// This base class is used for any CustomExceptions that are created.
    /// </summary>
    public abstract class CustomException : Exception
    {
        /// <summary>
        /// Return the StatusCode that the API should return when the Exception occurs.
        /// </summary>
        /// <returns>int representing the StatusCode associated with the Exception.</returns>
        public abstract int GetStatusCode();

        /// <summary>
        /// Returns the ResponseBody that should be returned when this Exception occurs.
        /// </summary>
        /// <returns>Object to be added to the API Response Body when this Exception occurs.</returns>
        public abstract object GetResponseBody();

        /// <summary>
        /// Returns the LogLevelConfigKey associated with this Exception which will determine what level the LogMessage is escalated to when this Exception occurs.
        /// </summary>
        /// <returns>The ConfigKey for the LogLevel that the LogMessage should be escalated to when this Exception occurs.</returns>
        public abstract string GetLogLevelConfigKey();
    }
}
