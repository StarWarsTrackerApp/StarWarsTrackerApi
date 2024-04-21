using StarWarsTracker.Domain.Enums;
using StarWarsTracker.Logging.Implementation;

namespace StarWarsTracker.Logging.Abstraction
{
    /// <summary>
    /// This interface defines the contract for how you can manipulate and obtain data from the LogMessage.    
    /// </summary>
    public interface ILogMessage
    {
        /// <summary>
        /// Add the LogContent to the Contents of the LogMessage. LogContent is not added if the LogLevel is None.
        /// </summary>
        /// <param name="logContent">The content to be added to the LogMessage</param>
        public void AddContent(LogContent logContent);

        /// <summary>
        /// Increase the Level of the LogMessage to the Level of the LogContent provided.
        /// If the Level is lower than the LogMessage is currently set, then the LogMessage will maintain the higher level.
        /// </summary>
        /// <param name="logContent">The LogContent to add to the LogMessage</param>
        public void IncreaseLevel(LogContent logContent);

        /// <summary>
        /// Returns the LogLevel that the LogMessage has been increased to. 
        /// </summary>
        /// <returns>LogLevel that the LogMessage has been increased to.</returns>
        public LogLevel GetLevel();

        /// <summary>
        /// Return the LogMessage as a JSON string with only LogContent that is above or equal to the LogLevel provided.
        /// </summary>
        /// <param name="logLevel">The lowest LogLevel of LogContent to be included in the response.</param>
        /// <returns>JSON string of the LogMessage with ElapsedMilliseconds, LogStartTime, LogEndTime, LogLevel, NameOfLogLevel, and LogContents.</returns>
        public string GetMessageJson(LogLevel logLevel);

        /// <summary>
        /// Returns all the LogContent that has been added to the LogMessage that is equal to or greater than the logLevel provided.
        /// </summary>
        /// <param name="logLevel">The lowest LogLevel of LogContent to be included in the response.</param>
        /// <returns>Collection of LogContent belonging to the ILogMessage that is equal to or greater than the logLevel provided.</returns>
        public IEnumerable<LogContent> GetContent(LogLevel logLevel);

        /// <summary>
        /// Returns the Elapsed Milliseconds since the LogMessage was initialized.
        /// </summary>
        /// <returns>Total Milliseconds since the LogMessage was initialized. </returns>
        public double GetElapsedMilliseconds();
    }
}
