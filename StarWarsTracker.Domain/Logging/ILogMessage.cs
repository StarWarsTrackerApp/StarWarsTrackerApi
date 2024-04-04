using StarWarsTracker.Domain.Enums;
using System.Runtime.CompilerServices;

namespace StarWarsTracker.Domain.Logging
{
    public interface ILogMessage
    {
        public void AddTrace<T>(T classCalling, string description, object? extra = null, [CallerMemberName] string methodCalling = "");

        public void AddDebug<T>(T classCalling, string description, object? extra = null, [CallerMemberName] string methodCalling = "");
        
        public void AddInfo<T>(T classCalling, string description, object? extra = null, [CallerMemberName] string methodCalling = "");
        
        public void AddWarning<T>(T classCalling, string description, object? extra = null, [CallerMemberName] string methodCalling = "");
        
        public void AddError<T>(T classCalling, string description, object? extra = null, [CallerMemberName] string methodCalling = "");
        
        public void AddCritical<T>(T classCalling, string description, object? extra = null, [CallerMemberName] string methodCalling = "");

        public void AddConfiguredLogLevel<T>(string logConfigSectionName, string logConfigName, T classCalling, string description, object? extra = null, [CallerMemberName] string methodCalling = "");

        public string GetLogsAsJson(LogLevel logLevel);

        public LogLevel GetLogLevel();

        public void IncreaseLevel<T>(LogLevel logLevel, T classCalling, string description, object? extra = null, [CallerMemberName] string methodCalling = "");
    }
}
