using StarWarsTracker.Domain.Enums;

namespace StarWarsTracker.Domain.Logging
{
    public interface ILogConfig
    {
        public LogLevel GetLogLevel(string section, string configKeyName);

        public void SetEndpointConfigs(string endpoint);
    }
}
