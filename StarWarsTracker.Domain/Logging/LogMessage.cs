using StarWarsTracker.Domain.Enums;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace StarWarsTracker.Domain.Logging
{
    public class LogMessage
    {
        #region Private Members

        private readonly string _startingMethod;

        private readonly List<LogContent> _logContents = new();

        private readonly DateTime _dateTimeCreatedUTC = DateTime.UtcNow;

        #endregion

        #region Constructor

        /// <summary>
        /// Private constructor is exposed via the public method New()
        /// </summary>
        /// <param name="startingMethod"></param>
        private LogMessage(string startingMethod) => _startingMethod = startingMethod;

        #endregion

        #region Exposed Methods

        /// <summary>
        /// Return a new instance of LogMessage with the methodCalling defaulting to the method that calls New().
        /// </summary>
        /// <param name="methodCalling">Defaults the methodCalling to the name of the method that calls New().</param>
        /// <returns></returns>
        public static LogMessage New([CallerMemberName] string methodCalling = "") => new(methodCalling);

        /// <summary>
        /// Add LogContent with the description and optional extra object provided. methodCalling defaults to the method that is calling AddTrace().
        /// The LogContent will be logged when the applications LogLevel is set to Trace.
        /// </summary>
        /// <param name="description">Message to add in the LogContent.</param>
        /// <param name="extra">Optional object that can be included in the LogContent.</param>
        /// <param name="methodCalling">Defaults to the method that is calling AddTrace()</param>
        public void AddTrace(string description, object? extra = null, [CallerMemberName] string methodCalling = "") => _logContents.Add(new (LogLevel.Trace, methodCalling, description, extra));

        /// <summary>
        /// Add LogContent with the description and optional extra object provided. methodCalling defaults to the method that is calling AddDebug().
        /// The LogContent will be logged when the applications LogLevel is set to Trace or Debug.
        /// </summary>
        /// <param name="description">Message to add in the LogContent.</param>
        /// <param name="extra">Optional object that can be included in the LogContent.</param>
        /// <param name="methodCalling">Defaults to the method that is calling AddDebug()</param>
        public void AddDebug(string description, object? extra = null, [CallerMemberName] string methodCalling = "") => _logContents.Add(new(LogLevel.Debug, methodCalling, description, extra));

        /// <summary>
        /// Add LogContent with the description and optional extra object provided. methodCalling defaults to the method that is calling AddInfo().
        /// The LogContent will be logged when the applications LogLevel is set to Trace, Debug or Info.
        /// </summary>
        /// <param name="description">Message to add in the LogContent.</param>
        /// <param name="extra">Optional object that can be included in the LogContent.</param>
        /// <param name="methodCalling">Defaults to the method that is calling AddInfo()</param>
        public void AddInfo(string description, object? extra = null, [CallerMemberName] string methodCalling = "") => _logContents.Add(new(LogLevel.Information, methodCalling, description, extra));

        /// <summary>
        /// Add LogContent with the description and optional extra object provided. methodCalling defaults to the method that is calling AddWarning().
        /// The LogContent will be logged when the applications LogLevel is set to Trace, Debug, Info, or Warning.
        /// </summary>
        /// <param name="description">Message to add in the LogContent.</param>
        /// <param name="extra">Optional object that can be included in the LogContent.</param>
        /// <param name="methodCalling">Defaults to the method that is calling AddWarning()</param>
        public void AddWarning(string description, object? extra = null, [CallerMemberName] string methodCalling = "") => _logContents.Add(new(LogLevel.Warning, methodCalling, description, extra));

        /// <summary>
        /// Add LogContent with the description and optional extra object provided. methodCalling defaults to the method that is calling AddError().
        /// The LogContent will be logged when the applications LogLevel is set to Trace, Debug, Info, Warning, or Error.
        /// </summary>
        /// <param name="description">Message to add in the LogContent.</param>
        /// <param name="extra">Optional object that can be included in the LogContent.</param>
        /// <param name="methodCalling">Defaults to the method that is calling AddError()</param>
        public void AddError(string description, object? extra = null, [CallerMemberName] string methodCalling = "") => _logContents.Add(new(LogLevel.Error, methodCalling, description, extra));

        /// <summary>
        /// Add LogContent with the description and optional extra object provided. methodCalling defaults to the method that is calling AddCritical().
        /// The LogContent will be logged when the applications LogLevel is set to Trace, Debug, Info, Warning, Error, or Critical.
        /// </summary>
        /// <param name="description">Message to add in the LogContent.</param>
        /// <param name="extra">Optional object that can be included in the LogContent.</param>
        /// <param name="methodCalling">Defaults to the method that is calling AddCritical()</param>
        public void AddCritical(string description, object? extra = null, [CallerMemberName] string methodCalling = "") => _logContents.Add(new(LogLevel.Critical, methodCalling, description, extra));

        #endregion

        /// <summary>
        /// Returns the LogMessage as a JSON containing the StartingMethod that created the log message, and all the LogContent that is above or equal to the logLevel provided.
        /// </summary>
        /// <param name="level">The LogLevel to log. If provided Info, then the Content will only contain Info and above.</param>
        /// <returns>JSON containing the StartingMethod, LogStartTime, and the LogContents with a LogLevel above or equal to the LogLevel provided.</returns>
        public string GetContentAsJson(LogLevel level)
        {
            var logContent = _logContents.Where(_ => _.LogLevel >= level);

            var content = new { StartingMethod = _startingMethod, LogStartTime = _dateTimeCreatedUTC, LogContents = logContent };

            return JsonSerializer.Serialize(content);
        }
    }
}
