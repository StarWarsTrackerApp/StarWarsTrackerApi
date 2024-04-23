using StarWarsTracker.Domain.Enums;
using StarWarsTracker.Logging.Abstraction;
using System.Diagnostics;

namespace StarWarsTracker.Logging.Implementation
{
    internal class LogMessage : ILogMessage
    {
        #region Private Members

        private readonly List<LogContent> _logContents = new();

        private LogLevel _logLevel = LogLevel.Trace;

        private readonly Stopwatch _stopwatch = Stopwatch.StartNew();

        #endregion

        #region Public ILogMessage Methods

        public void AddContent(LogContent logContent)
        {
            if (logContent.LogLevel != LogLevel.None)
            {
                _logContents.Add(logContent);
            }
        }

        public double GetElapsedMilliseconds() => _stopwatch.Elapsed.TotalMilliseconds;

        public LogLevel GetLevel() => _logLevel;
       
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

        public IEnumerable<LogContent> GetAllContent() => _logContents;

        #endregion
    }
}
