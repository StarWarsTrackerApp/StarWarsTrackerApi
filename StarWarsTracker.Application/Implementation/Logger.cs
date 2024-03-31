using StarWarsTracker.Domain.Logging;
using StarWarsTracker.Persistence.DataRequestObjects.Logging;
using System.Runtime.CompilerServices;

namespace StarWarsTracker.Application.Implementation
{
    public class Logger<T> : ILogger<T>
    {
        #region Private Members

        private readonly LogLevel _logLevel;

        private readonly IDataAccess _dataAccess;

        private readonly string _className;

        #endregion

        #region Constructor

        public Logger(LogLevel logLevel, IDataAccess dataAccess)
        {
            _logLevel = logLevel;

            _dataAccess = dataAccess;

            _className = typeof(T).Name;
        }

        #endregion

        #region Public Methods

        public void LogTrace(LogMessage message, string? stackTrace = null, [CallerMemberName] string methodName = "") => Log(LogLevel.Trace, message, stackTrace, methodName);

        public void LogDebug(LogMessage message, string? stackTrace = null, [CallerMemberName] string methodName = "") => Log(LogLevel.Debug, message, stackTrace, methodName);

        public void LogInfo(LogMessage message, string? stackTrace = null, [CallerMemberName] string methodName = "") => Log(LogLevel.Information, message, stackTrace, methodName);

        public void LogWarning(LogMessage message, string? stackTrace = null, [CallerMemberName] string methodName = "") => Log(LogLevel.Warning, message, stackTrace, methodName);

        public void LogError(LogMessage message, string? stackTrace = null, [CallerMemberName] string methodName = "") => Log(LogLevel.Error, message, stackTrace, methodName);

        public void LogCritical(LogMessage message, string? stackTrace = null, [CallerMemberName] string methodName = "") => Log(LogLevel.Critical, message, stackTrace, methodName);

        #endregion

        #region Private Methods

        private void Log(LogLevel logLevel, LogMessage logMessage, string? stackTrace = null, [CallerMemberName] string methodName = "")
        {
            if (_logLevel == LogLevel.None)
            {
                return;
            }

            if (_logLevel <= logLevel)
            {
                var message = logMessage.GetContentAsJson(_logLevel);

                Task.Run(() => _dataAccess.ExecuteAsync(new InsertLog((int)logLevel, message, _className, methodName, stackTrace)));
            }
        }

        #endregion
    }
}
