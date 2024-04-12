using StarWarsTracker.Domain.Enums;
using StarWarsTracker.Logging.Abstraction;
using System.Diagnostics;
using System.Text.Json;

namespace StarWarsTracker.Logging.Implementation
{
    internal class LogMessage : ILogMessage
    {
        #region Private Members

        private readonly List<LogContent> _logContents = new();

        private LogLevel _logLevel = LogLevel.Trace;

        private readonly DateTime _dateTimeCreatedUTC = DateTime.UtcNow;

        private readonly Stopwatch _stopwatch = Stopwatch.StartNew();

        #endregion

        #region Public ILogMessage Methods

        /// <summary>
        /// Add the LogContent to the Contents of the LogMessage. LogContent is not added if the LogLevel is None.
        /// </summary>
        /// <param name="logContent">The content to be added to the LogMessage</param>
        public void AddContent(LogContent logContent)
        {
            if (logContent.LogLevel != LogLevel.None)
            {
                _logContents.Add(logContent);
            }
        }

        /// <summary>
        /// Return the LogMessage as a JSON string with only LogContent that is above or equal to the LogLevel provided 
        /// </summary>
        /// <param name="logLevel">The lowest LogLevel of LogContent to be included in the response.</param>
        /// <returns>JSON string of the LogMessage with ElapsedMilliseconds, LogStartTime, LogEndTime, LogLevel, NameOfLogLevel, and LogContents.</returns>
        public string GetMessageJson(LogLevel logLevel)
        {
            if (logLevel == LogLevel.None)
            {
                return string.Empty;
            }

            var messagesToLog = _logContents.Where(_ => _.LogLevel >= logLevel);

            var currentTime = DateTime.UtcNow;

            var logContent = new
            {
                ElapsedMilliseconds = GetElapsedMilliseconds(),
                LogStartTime = _dateTimeCreatedUTC,
                LogEndTime = currentTime,
                LogLevel = _logLevel,
                NameOfLogLevel = _logLevel.ToString(),
                LogContents = messagesToLog
            };

            var jsonContent = JsonSerializer.Serialize(logContent);

            return jsonContent;
        }

        /// <summary>
        /// Returns the Elapsed Milliseconds since the LogMessage was initialized.
        /// </summary>
        /// <returns>Total Milliseconds since the LogMessage was initialized. </returns>
        public double GetElapsedMilliseconds() => _stopwatch.Elapsed.TotalMilliseconds;

        public LogLevel GetLevel() => _logLevel;

        /// <summary>
        /// Increase the Level of the LogMessage to the Level of the LogContent provided.
        /// If the Level is lower than the LogMessage is currently set, then the LogMessage will maintain the higher level.
        /// </summary>
        /// <param name="logContent">The LogContent to add to the LogMessage</param>
        public void IncreaseLevel(LogContent logContent)
        {
            if (logContent.LogLevel == LogLevel.None)
            {
                return;
            }

            AddContent(logContent);

            if (logContent.LogLevel > _logLevel)
            {
                _logLevel = logContent.LogLevel;
            }
        }

        public IEnumerable<LogContent> GetContent(LogLevel logLevel) =>
            logLevel == LogLevel.None ? Enumerable.Empty<LogContent>() : _logContents.Where(_ => _.LogLevel >= logLevel);

        #endregion
    }
}
