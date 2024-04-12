using StarWarsTracker.Domain.Constants.LogConfigs;
using StarWarsTracker.Domain.Enums;
using StarWarsTracker.Logging.Abstraction;
using System.Runtime.CompilerServices;

namespace StarWarsTracker.Logging.Implementation
{
    internal class ClassLogger : IClassLogger
    {
        #region Private Members

        private readonly ILogMessage _logMessage;

        private readonly ILogConfigReader _logConfigReader;

        private readonly string _className, _namespaceName;

        private readonly LogLevel _trace, _debug , _info, _warning, _error, _critical;

        #endregion

        #region Constructor

        internal ClassLogger(string className, string? namespaceName, ILogMessage logMessage, ILogConfigReader logConfigReader)
        {
            _className = className;
            _namespaceName = namespaceName ?? "Unknown";
            _logMessage = logMessage;
            _logConfigReader = logConfigReader;

            var classOverrides = _logConfigReader.GetConfigSection(Category.OverrideLogLevelByClassName, _className);
            var nameSpaceOverrides = GetNameSpaceOverrides();

            _trace = GetOverrideOrLevel(nameSpaceOverrides, classOverrides, LogLevel.Trace);
            _debug = GetOverrideOrLevel(nameSpaceOverrides, classOverrides, LogLevel.Debug);
            _info = GetOverrideOrLevel(nameSpaceOverrides, classOverrides, LogLevel.Information);
            _warning = GetOverrideOrLevel(nameSpaceOverrides, classOverrides, LogLevel.Warning);
            _error = GetOverrideOrLevel(nameSpaceOverrides, classOverrides, LogLevel.Error);
            _critical = GetOverrideOrLevel(nameSpaceOverrides, classOverrides, LogLevel.Critical);
        }

        #endregion

        #region Public IClassLogger Methods

        /// <summary>
        /// Add the Description and optional extra object as LogContent to the LogMessage. 
        /// The LogContent will be Trace unless there is a namespace or class override. 
        /// Class overrides take priority over namespace overrides.
        /// </summary>
        /// <param name="description">The description to add to the LogContent. </param>
        /// <param name="extra">Optional object to be added to the LogContent. </param>
        /// <param name="methodCalling">Defaults to the name of the method that calls AddTrace().</param>
        public void AddTrace(string description, object? extra = null, [CallerMemberName] string methodCalling = "") =>
            AddContent(_trace, description, extra, methodCalling);

        /// <summary>
        /// Add the Description and optional extra object as LogContent to the LogMessage. 
        /// The LogContent will be Debug unless there is a namespace or class override. 
        /// Class overrides take priority over namespace overrides.
        /// </summary>
        /// <param name="description">The description to add to the LogContent. </param>
        /// <param name="extra">Optional object to be added to the LogContent. </param>
        /// <param name="methodCalling">Defaults to the name of the method that calls AddDebug().</param>
        public void AddDebug(string description, object? extra = null, [CallerMemberName] string methodCalling = "") =>
            AddContent(_debug, description, extra, methodCalling);

        /// <summary>
        /// Add the Description and optional extra object as LogContent to the LogMessage. 
        /// The LogContent will be Info unless there is a namespace or class override. 
        /// Class overrides take priority over namespace overrides.
        /// </summary>
        /// <param name="description">The description to add to the LogContent. </param>
        /// <param name="extra">Optional object to be added to the LogContent. </param>
        /// <param name="methodCalling">Defaults to the name of the method that calls AddInfo().</param>
        public void AddInfo(string description, object? extra = null, [CallerMemberName] string methodCalling = "") =>
            AddContent(_info, description, extra, methodCalling);

        /// <summary>
        /// Add the Description and optional extra object as LogContent to the LogMessage. 
        /// The LogContent will be Warning unless there is a namespace or class override. 
        /// Class overrides take priority over namespace overrides.
        /// </summary>
        /// <param name="description">The description to add to the LogContent. </param>
        /// <param name="extra">Optional object to be added to the LogContent. </param>
        /// <param name="methodCalling">Defaults to the name of the method that calls AddWarning().</param>
        public void AddWarning(string description, object? extra = null, [CallerMemberName] string methodCalling = "") =>
            AddContent(_warning, description, extra, methodCalling);

        /// <summary>
        /// Add the Description and optional extra object as LogContent to the LogMessage. 
        /// The LogContent will be Error unless there is a namespace or class override. 
        /// Class overrides take priority over namespace overrides.
        /// </summary>
        /// <param name="description">The description to add to the LogContent. </param>
        /// <param name="extra">Optional object to be added to the LogContent. </param>
        /// <param name="methodCalling">Defaults to the name of the method that calls AddError().</param>
        public void AddError(string description, object? extra = null, [CallerMemberName] string methodCalling = "") =>
            AddContent(_error, description, extra, methodCalling);

        /// <summary>
        /// Add the Description and optional extra object as LogContent to the LogMessage. 
        /// The LogContent will be Critical unless there is a namespace or class override. 
        /// Class overrides take priority over namespace overrides.
        /// </summary>
        /// <param name="description">The description to add to the LogContent. </param>
        /// <param name="extra">Optional object to be added to the LogContent. </param>
        /// <param name="methodCalling">Defaults to the name of the method that calls AddCritical().</param>
        public void AddCritical(string description, object? extra = null, [CallerMemberName] string methodCalling = "") =>
            AddContent(_critical, description, extra, methodCalling);

        /// <summary>
        /// Add the description and optional extra object as LogContent to the LogMessage.
        /// The LogContent will be a loglevel that is configured under CustomLogLevels with the logConfigSection and logConfigKey provided.
        /// Class and namespace overrides do not impact the LogLevel used for LogContent added with this method.
        /// </summary>
        /// <param name="logConfigSection">The ConfigSection from the CustomLogLevel ConfigCategory that the ConfigKey belongs to.</param>      
        /// <param name="logConfigKey">The ConfigKey that configures the LogLevel the LogContent added will be.</param>
        /// <param name="description">The description to add to the LogContent. </param>
        /// <param name="extra">Optional object to be added to the LogContent. </param>
        /// <param name="methodCalling">Defaults to the name of the method that calls AddCritical().</param>
        public void AddConfiguredLogLevel(string logConfigSection, string logConfigKey, string description, object? extra = null, [CallerMemberName] string methodCalling = "") =>            
            AddContent(_logConfigReader.GetCustomLogLevel(logConfigSection, logConfigKey) ?? LogLevel.None, description, extra, methodCalling);

        /// <summary>
        /// Increases the LogMessage to the level provided. Adds the Description and optional extra object as LogContent to the LogMessage. 
        /// The LogContent will be the same level as the LogMessage is being increased to. 
        /// Class and namespace overrides do not impact the LogLevel used for LogContent added with this method.
        /// </summary>
        /// <param name="logLevel"></param>
        /// <param name="description"></param>
        /// <param name="extra"></param>
        /// <param name="methodCalling"></param>
        public void IncreaseLevel(LogLevel logLevel, string description, object? extra = null, [CallerMemberName] string methodCalling = "") =>
            _logMessage.IncreaseLevel(new(logLevel, _className, _namespaceName, methodCalling, description, extra, _logMessage.GetElapsedMilliseconds()));

        #endregion

        #region Private Helper Methods

        /// <summary>
        /// Helper class for adding LogContent to the LogMessage
        /// </summary>
        private void AddContent(LogLevel logLevel, string description, object? extra, string methodName) =>
            _logMessage.AddContent(new(logLevel, _className, _namespaceName, methodName, description, extra, _logMessage.GetElapsedMilliseconds()));

        /// <summary>
        /// Helper for setting overrides for LogLevels by NameSpace and ClassName. Class Overrides take priority over NameSpace Overrides.
        /// </summary>
        /// <param name="nameSpaceOverrides"></param>
        /// <param name="classOverrides"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        private static LogLevel GetOverrideOrLevel(Dictionary<string, LogLevel>? nameSpaceOverrides, Dictionary<string, LogLevel>? classOverrides, LogLevel level) =>
            classOverrides?.TryGetValue(level.ToString(), out var classLevel) ?? false ? classLevel
            : nameSpaceOverrides?.TryGetValue(level.ToString(), out var nameSpaceLevel) ?? false ? nameSpaceLevel 
            : level;

        /// <summary>
        /// Helper to get the NameSpace Overrides. If more than one namespace matches, then will use the longest match.
        /// </summary>
        /// <returns>Dictionary of the LogLevel overrides by NameSpace </returns>
        private Dictionary<string, LogLevel>? GetNameSpaceOverrides()
        {
            var allNameSpaceOverrides = _logConfigReader.GetConfigCategory(Category.OverrideLogLevelByNameSpace);

            Dictionary<string, LogLevel> nameSpaceOverrides = null!;

            var qualifyingNameSpaceOverrides = allNameSpaceOverrides?.Where(_ => _namespaceName.Equals(_.Key, StringComparison.OrdinalIgnoreCase));

            if (qualifyingNameSpaceOverrides?.Any() ?? false)
            {
                nameSpaceOverrides = qualifyingNameSpaceOverrides.OrderBy(_ => _.Key.Length).First().Value;
            }

            return nameSpaceOverrides;
        }
        
        #endregion
    }
}    
