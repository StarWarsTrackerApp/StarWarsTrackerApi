using StarWarsTracker.Domain.Enums;

namespace StarWarsTracker.Logging.Abstraction
{
    public interface ILogConfigReader
    {
        public LogLevel? GetCustomLogLevel(string configSection, string configKeyName);

        public bool TrySetEndpointConfigs(string endpoint);

        public Dictionary<string, Dictionary<string, LogLevel>>? GetConfigCategory(string configCategory);

        public Dictionary<string, LogLevel>? GetConfigSection(string configCategory, string configSection);

        public Dictionary< string, Dictionary<string, Dictionary<string, LogLevel>>> GetActiveConfigs();
    }
}
