using System.Runtime.CompilerServices;

namespace StarWarsTracker.Domain.Logging
{
    public interface ILogger<T>
    {
        /// <summary>
        /// Logs the LogMessage with the LogLevel Trace when the applications LogLevel is set to Trace
        /// </summary>
        /// <param name="message">LogMessage to log - Will log all LogContent with LogLevels above or equal to Trace.</param>
        /// <param name="stackTrace">optional stackTrace to log with the LogMessage.</param>
        /// <param name="methodName">Defaults to the method that is calling LogTrace().</param>
        public void LogTrace(LogMessage message, string? stackTrace = null, [CallerMemberName] string methodName = "");

        /// <summary>
        /// Logs the LogMessage with the LogLevel Debug when the applications LogLevel is set to Trace or Debug
        /// </summary>
        /// <param name="message">LogMessage to log - Will log all LogContent with LogLevels above or equal to Debug.</param>
        /// <param name="stackTrace">optional stackTrace to log with the LogMessage.</param>
        /// <param name="methodName">Defaults to the method that is calling LogDebug().</param>
        public void LogDebug(LogMessage message, string? stackTrace = null, [CallerMemberName] string methodName = "");

        /// <summary>
        /// Logs the LogMessage with the LogLevel Info when the applications LogLevel is set to Trace, Debug, or Info
        /// </summary>
        /// <param name="message">LogMessage to log - Will log all LogContent with LogLevels above or equal to Info.</param>
        /// <param name="stackTrace">optional stackTrace to log with the LogMessage.</param>
        /// <param name="methodName">Defaults to the method that is calling LogInfo().</param>
        public void LogInfo(LogMessage message, string? stackTrace = null, [CallerMemberName] string methodName = "");

        /// <summary>
        /// Logs the LogMessage with the LogLevel Warning when the applications LogLevel is set to Trace, Debug, Info, or Warning
        /// </summary>
        /// <param name="message">LogMessage to log - Will log all LogContent with LogLevels above or equal to Warning.</param>
        /// <param name="stackTrace">optional stackTrace to log with the LogMessage.</param>
        /// <param name="methodName">Defaults to the method that is calling LogWarning().</param>
        public void LogWarning(LogMessage message, string? stackTrace = null, [CallerMemberName] string methodName = "");

        /// <summary>
        /// Logs the LogMessage with the LogLevel Error when the applications LogLevel is set to Trace, Debug, Info, Warning or Error
        /// </summary>
        /// <param name="message">LogMessage to log - Will log all LogContent with LogLevels above or equal to Error.</param>
        /// <param name="stackTrace">optional stackTrace to log with the LogMessage.</param>
        /// <param name="methodName">Defaults to the method that is calling LogError().</param>
        public void LogError(LogMessage message, string? stackTrace = null, [CallerMemberName] string methodName = "");

        /// <summary>
        /// Logs the LogMessage with the LogLevel Critical when the applications LogLevel is set to Trace, Debug, Info, Warning, Error, or Critical
        /// </summary>
        /// <param name="message">LogMessage to log - Will log all LogContent with LogLevels above or equal to Error.</param>
        /// <param name="stackTrace">optional stackTrace to log with the LogMessage.</param>
        /// <param name="methodName">Defaults to the method that is calling LogCritical().</param>
        public void LogCritical(LogMessage message, string? stackTrace = null, [CallerMemberName] string methodName = "");
    }
}
