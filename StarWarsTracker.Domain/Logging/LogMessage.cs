using StarWarsTracker.Domain.Constants;
using StarWarsTracker.Domain.Enums;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace StarWarsTracker.Domain.Logging
{
    public class LogMessage : ILogMessage
    {
        #region Private Members

        private readonly List<LogContent> _logContents = new();

        private readonly DateTime _dateTimeCreatedUTC = DateTime.UtcNow;

        private LogLevel _logLevel = LogLevel.Trace;

        private readonly Stopwatch _stopwatch = Stopwatch.StartNew();

        private readonly ILogConfig _logConfig;

        #endregion

        public LogMessage(ILogConfig logConfig) => _logConfig = logConfig;

        #region Exposed Methods

        private void Add<T>(LogLevel logLevel, T classCalling, string description, object? extra = null, [CallerMemberName] string methodCalling = "")
        {
            var classType = classCalling?.GetType();

            var className = classType?.Name ?? "Unknown";
            var nameSpace = classType?.Namespace ?? "Unknown";

            if(logLevel != LogLevel.None)
            {
                _logContents.Add(new(logLevel, className, nameSpace, methodCalling, description, extra, _stopwatch.ElapsedMilliseconds));
            }
        }

        public void AddTrace<T>(T classCalling, string description, object? extra = null, [CallerMemberName] string methodCalling = "") => 
            Add(LogLevel.Trace, classCalling, description, extra, methodCalling);

        public void AddDebug<T>(T classCalling, string description, object? extra = null, [CallerMemberName] string methodCalling = "") =>
            Add(LogLevel.Debug, classCalling, description, extra, methodCalling);

        public void AddInfo<T>(T classCalling, string description, object? extra = null, [CallerMemberName] string methodCalling = "") => 
            Add(LogLevel.Information, classCalling, description, extra, methodCalling);

        public void AddWarning<T>(T classCalling, string description, object? extra = null, [CallerMemberName] string methodCalling = "") => 
            Add(LogLevel.Warning, classCalling, description, extra, methodCalling);

        public void AddError<T>(T classCalling, string description, object? extra = null, [CallerMemberName] string methodCalling = "") =>
            Add(LogLevel.Error, classCalling, description, extra, methodCalling);

        public void AddCritical<T>(T classCalling, string description, object? extra = null, [CallerMemberName] string methodCalling = "") =>
            Add(LogLevel.Critical, classCalling, description, extra, methodCalling);

        public void AddConfiguredLogLevel<T>(string logConfigSectionName, string logConfigName, T classCalling, string description, object? extra = null, [CallerMemberName] string methodCalling = "") =>
            Add(_logConfig.GetLogLevel(logConfigSectionName, logConfigName), classCalling, description, extra, methodCalling);
            
        public string GetLogsAsJson(LogLevel logLevel)
        {
            var messagesToLog = _logContents.Where(_ => _.LogLevel >= logLevel);

            var currentTime = DateTime.UtcNow;

            var logContent = new
            {
                _stopwatch.ElapsedMilliseconds,
                LogStartTime = _dateTimeCreatedUTC,
                LogEndTime = currentTime,
                LogLevel = _logLevel,
                NameOfLogLevel = _logLevel.ToString(),                
                LogContents = messagesToLog
            };

            var jsonContent = JsonSerializer.Serialize(logContent);

            return jsonContent;
        }

        #endregion

        public LogLevel GetLogLevel() => _logLevel;

        public void IncreaseLevel<T>(LogLevel logLevel, T classCalling, string description, object? extra = null, [CallerMemberName] string methodCalling = "")
        {
            if (logLevel == LogLevel.None)
            {
                return;
            }

            Add(logLevel, classCalling, description, extra, methodCalling);

            if (logLevel > _logLevel)
            {
                _logLevel = logLevel;
            }
        }
    }
}
