using StarWarsTracker.Domain.Enums;
using System.Diagnostics;
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

        #region Constructor

        public LogMessage(ILogConfig logConfig) => _logConfig = logConfig;

        #endregion

        #region Public Methods Adding Logs

        /// <summary>
        /// Increase the LogLevel of the LogMessage and add LogContent with the same LogLevel.
        /// Content should be added that would be okay to see in Production and explains why the LogMessage is increasing in level.
        /// </summary>
        /// <typeparam name="T">The Type of the class that is adding to the LogMessage. </typeparam>
        /// <param name="logLevel">The LogLevel to increase the LogMessage to.</param>
        /// <param name="classCalling">The class that is adding to the LogMessage. </param>
        /// <param name="description">The description or message to add to the LogMessage.</param>
        /// <param name="extra">Optional Object to be deserialized/stored with the LogMessage.</param>
        /// <param name="methodCalling">Defaults to the name of the method that is adding to the LogMessage.</param>
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

        /// <summary>
        /// Add a section of LogContent to the LogMessage. The LogContent will be at a Trace LogLevel. 
        /// Any level of information can be stored with this type of level, and these logs should provide a step by step summary of transactions.
        /// </summary>
        /// <typeparam name="T">The Type of the class that is adding to the LogMessage. </typeparam>
        /// <param name="classCalling">The class that is adding to the LogMessage. </param>
        /// <param name="description">The description or message to add to the LogMessage.</param>
        /// <param name="extra">Optional Object to be deserialized/stored with the LogMessage.</param>
        /// <param name="methodCalling">Defaults to the name of the method that is adding to the LogMessage.</param>
        public void AddTrace<T>(T classCalling, string description, object? extra = null, [CallerMemberName] string methodCalling = "") =>
            Add(LogLevel.Trace, classCalling, description, extra, methodCalling);

        /// <summary>
        /// Add a section of LogContent to the LogMessage. The LogContent will be at a Debug LogLevel. 
        /// Any level of information can be stored here, but these logs should specifically have details that would help when debugging.
        /// </summary>
        /// <typeparam name="T">The Type of the class that is adding to the LogMessage. </typeparam>
        /// <param name="classCalling">The class that is adding to the LogMessage. </param>
        /// <param name="description">The description or message to add to the LogMessage.</param>
        /// <param name="extra">Optional Object to be deserialized/stored with the LogMessage.</param>
        /// <param name="methodCalling">Defaults to the name of the method that is adding to the LogMessage.</param>
        public void AddDebug<T>(T classCalling, string description, object? extra = null, [CallerMemberName] string methodCalling = "") =>
            Add(LogLevel.Debug, classCalling, description, extra, methodCalling);

        /// <summary>
        /// Add a section of LogContent to the LogMessage. The LogContent will be at a Information LogLevel. 
        /// You should only add content that you would be okay having in Production logs. These logs should help give a high level summary of the transaction.
        /// </summary>
        /// <typeparam name="T">The Type of the class that is adding to the LogMessage. </typeparam>
        /// <param name="classCalling">The class that is adding to the LogMessage. </param>
        /// <param name="description">The description or message to add to the LogMessage.</param>
        /// <param name="extra">Optional Object to be deserialized/stored with the LogMessage.</param>
        /// <param name="methodCalling">Defaults to the name of the method that is adding to the LogMessage.</param>
        public void AddInfo<T>(T classCalling, string description, object? extra = null, [CallerMemberName] string methodCalling = "") =>
            Add(LogLevel.Information, classCalling, description, extra, methodCalling);

        /// <summary>
        /// Add a section of LogContent to the LogMessage. The LogContent will be at a Warning LogLevel. 
        /// You should only add content that you would be okay having in Production logs. These logs should give information around a possible issue.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T">The Type of the class that is adding to the LogMessage. </typeparam>
        /// <param name="classCalling">The class that is adding to the LogMessage. </param>
        /// <param name="description">The description or message to add to the LogMessage.</param>
        /// <param name="extra">Optional Object to be deserialized/stored with the LogMessage.</param>
        /// <param name="methodCalling">Defaults to the name of the method that is adding to the LogMessage.</param>
        public void AddWarning<T>(T classCalling, string description, object? extra = null, [CallerMemberName] string methodCalling = "") =>
            Add(LogLevel.Warning, classCalling, description, extra, methodCalling);

        /// <summary>
        /// Add a section of LogContent to the LogMessage. The LogContent will be at an Error LogLevel.
        /// You should only add content that you would be okay having in Production logs. These logs should give information around a non-critical issue.
        /// </summary>
        /// <typeparam name="T">The Type of the class that is adding to the LogMessage. </typeparam>
        /// <param name="classCalling">The class that is adding to the LogMessage. </param>
        /// <param name="description">The description or message to add to the LogMessage.</param>
        /// <param name="extra">Optional Object to be deserialized/stored with the LogMessage.</param>
        /// <param name="methodCalling">Defaults to the name of the method that is adding to the LogMessage.</param>
        public void AddError<T>(T classCalling, string description, object? extra = null, [CallerMemberName] string methodCalling = "") =>
            Add(LogLevel.Error, classCalling, description, extra, methodCalling);

        /// <summary>
        /// Add a section of LogContent to the LogMessage. The LogContent will be at a Critical LogLevel.
        /// You should only add content that you would be okay having in Production logs. These logs should given information around a critical issue.
        /// </summary>
        /// <typeparam name="T">The Type of the class that is adding to the LogMessage. </typeparam>
        /// <param name="classCalling">The class that is adding to the LogMessage. </param>
        /// <param name="description">The description or message to add to the LogMessage.</param>
        /// <param name="extra">Optional Object to be deserialized/stored with the LogMessage.</param>
        /// <param name="methodCalling">Defaults to the name of the method that is adding to the LogMessage.</param>
        public void AddCritical<T>(T classCalling, string description, object? extra = null, [CallerMemberName] string methodCalling = "") =>
            Add(LogLevel.Critical, classCalling, description, extra, methodCalling);

        /// <summary>
        /// Add a section of LogContent to the LogMessage. The LogLevel for the content will be determined by the configSectionName/configName provided.
        /// You should use this for content that you want to be able to configure the LogLevel within your appsettings in order to enable/disable at different levels.
        /// </summary>
        /// <typeparam name="T">The Type of the class that is adding to the LogMessage. </typeparam>
        /// <param name="logConfigSectionName"></param>
        /// <param name="logConfigName"></param>
        /// <param name="classCalling">The class that is adding to the LogMessage. </param>
        /// <param name="description">The description or message to add to the LogMessage.</param>
        /// <param name="extra">Optional Object to be deserialized/stored with the LogMessage.</param>
        /// <param name="methodCalling">Defaults to the name of the method that is adding to the LogMessage.</param>
        public void AddConfiguredLogLevel<T>(string logConfigSectionName, string logConfigName, T classCalling, string description, object? extra = null, [CallerMemberName] string methodCalling = "") =>
            Add(_logConfig.GetLogLevel(logConfigSectionName, logConfigName), classCalling, description, extra, methodCalling);

        /// <summary>
        /// Returns the LogLevel of the message itself. This is increased when the IncreaseLevel method is used to add content to a LogMessage.
        /// </summary>
        /// <returns></returns>
        public LogLevel GetLogLevel() => _logLevel;

        /// <summary>
        /// Returns the LogMessage as a JSON string, with logContents only equal or above the LogLevel provided.
        /// </summary>
        /// <param name="logLevel">The minimum LogLevel a LogContent must be in order to be included in the json returned.</param>
        /// <returns>
        /// JSON string with Log Start/End Time, ElapsedMilliseconds, Message LogLevel, and the LogContents that are above the LogLevel provided.
        /// </returns>
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

        /// <summary>
        /// Helper for adding LogContent to the LogMessage. Will Also add the Class Name/NameSpace, and ElapsedMilliseconds since logging began.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="logLevel">The LogLevel to add the LogContent as. </param>
        /// <param name="classCalling">The class that is adding to the LogMessage. </param>
        /// <param name="description">The description or message to add to the LogMessage.</param>
        /// <param name="extra">Optional Object to be deserialized/stored with the LogMessage.</param>
        /// <param name="methodCalling">Defaults to the name of the method that is adding to the LogMessage.</param>
        private void Add<T>(LogLevel logLevel, T classCalling, string description, object? extra, string methodCalling)
        {
            var classType = classCalling?.GetType();

            var className = classType?.Name ?? "Unknown";
            var nameSpace = classType?.Namespace ?? "Unknown";

            if (logLevel != LogLevel.None)
            {
                _logContents.Add(new(logLevel, className, nameSpace, methodCalling, description, extra, _stopwatch.ElapsedMilliseconds));
            }
        }
    }
}
