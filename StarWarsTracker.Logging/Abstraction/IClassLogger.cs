using StarWarsTracker.Domain.Enums;
using System.Runtime.CompilerServices;

namespace StarWarsTracker.Logging.Abstraction
{
    public interface IClassLogger
    {
        public void IncreaseLevel(LogLevel logLevel, string description, object? extra = null, [CallerMemberName] string methodCalling = "");

        public void AddTrace(string description, object? extra = null, [CallerMemberName] string methodCalling = "");

        public void AddDebug(string description, object? extra = null, [CallerMemberName] string methodCalling = "");

        public void AddInfo(string description, object? extra = null, [CallerMemberName] string methodCalling = "");

        public void AddWarning(string description, object? extra = null, [CallerMemberName] string methodCalling = "");

        public void AddError(string description, object? extra = null, [CallerMemberName] string methodCalling = "");

        public void AddCritical(string description, object? extra = null, [CallerMemberName] string methodCalling = "");

        public void AddConfiguredLogLevel(string logConfigSection, string logConfigKey, string description, object? extra = null, [CallerMemberName] string methodCalling = "");
    }
}
