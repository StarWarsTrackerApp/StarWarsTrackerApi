using StarWarsTracker.Domain.Enums;
using System.Runtime.CompilerServices;

namespace StarWarsTracker.Logging.Abstraction
{
    /// <summary>
    /// This interface defines the contract of how a Class will be able to manipulate a LogMessage.
    /// There is standard methods for AddTrace, AddDebug, etc. as well as a method for configured logging which is set in appSettings.
    /// The method to IncreaseLevel can raise the level of the LogMessage, which is a separate level than each of the contents being added to it.    
    /// </summary>
    public interface IClassLogger
    {
        /// <summary>
        /// Increases the LogMessage to the level provided. Adds the Description and optional extra object as LogContent to the LogMessage. 
        /// The LogContent will be the same level as the LogMessage is being increased to. 
        /// Class and namespace overrides do not impact the LogLevel used for LogContent added with this method.
        /// </summary>
        /// <param name="logLevel"></param>
        /// <param name="description"></param>
        /// <param name="extra"></param>
        /// <param name="methodCalling"></param>
        public void IncreaseLevel(LogLevel logLevel, string description, object? extra = null, [CallerMemberName] string methodCalling = "");

        /// <summary>
        /// Add the Description and optional extra object as LogContent to the LogMessage. 
        /// The LogContent will be Trace unless there is a namespace or class override. 
        /// Class overrides take priority over namespace overrides.
        /// </summary>
        /// <param name="description">The description to add to the LogContent. </param>
        /// <param name="extra">Optional object to be added to the LogContent. </param>
        /// <param name="methodCalling">Defaults to the name of the method that calls AddTrace().</param>
        public void AddTrace(string description, object? extra = null, [CallerMemberName] string methodCalling = "");
       
        /// <summary>
        /// Add the Description and optional extra object as LogContent to the LogMessage. 
        /// The LogContent will be Debug unless there is a namespace or class override. 
        /// Class overrides take priority over namespace overrides.
        /// </summary>
        /// <param name="description">The description to add to the LogContent. </param>
        /// <param name="extra">Optional object to be added to the LogContent. </param>
        /// <param name="methodCalling">Defaults to the name of the method that calls AddDebug().</param>
        public void AddDebug(string description, object? extra = null, [CallerMemberName] string methodCalling = "");

        /// <summary>
        /// Add the Description and optional extra object as LogContent to the LogMessage. 
        /// The LogContent will be Info unless there is a namespace or class override. 
        /// Class overrides take priority over namespace overrides.
        /// </summary>
        /// <param name="description">The description to add to the LogContent. </param>
        /// <param name="extra">Optional object to be added to the LogContent. </param>
        /// <param name="methodCalling">Defaults to the name of the method that calls AddInfo().</param>
        public void AddInfo(string description, object? extra = null, [CallerMemberName] string methodCalling = "");

        /// <summary>
        /// Add the Description and optional extra object as LogContent to the LogMessage. 
        /// The LogContent will be Warning unless there is a namespace or class override. 
        /// Class overrides take priority over namespace overrides.
        /// </summary>
        /// <param name="description">The description to add to the LogContent. </param>
        /// <param name="extra">Optional object to be added to the LogContent. </param>
        /// <param name="methodCalling">Defaults to the name of the method that calls AddWarning().</param>
        public void AddWarning(string description, object? extra = null, [CallerMemberName] string methodCalling = "");

        /// <summary>
        /// Add the Description and optional extra object as LogContent to the LogMessage. 
        /// The LogContent will be Error unless there is a namespace or class override. 
        /// Class overrides take priority over namespace overrides.
        /// </summary>
        /// <param name="description">The description to add to the LogContent. </param>
        /// <param name="extra">Optional object to be added to the LogContent. </param>
        /// <param name="methodCalling">Defaults to the name of the method that calls AddError().</param>
        public void AddError(string description, object? extra = null, [CallerMemberName] string methodCalling = "");

        /// <summary>
        /// Add the Description and optional extra object as LogContent to the LogMessage. 
        /// The LogContent will be Critical unless there is a namespace or class override. 
        /// Class overrides take priority over namespace overrides.
        /// </summary>
        /// <param name="description">The description to add to the LogContent. </param>
        /// <param name="extra">Optional object to be added to the LogContent. </param>
        /// <param name="methodCalling">Defaults to the name of the method that calls AddCritical().</param>
        public void AddCritical(string description, object? extra = null, [CallerMemberName] string methodCalling = "");

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
        public void AddConfiguredLogLevel(string logConfigSection, string logConfigKey, string description, object? extra = null, [CallerMemberName] string methodCalling = "");
    }
}
