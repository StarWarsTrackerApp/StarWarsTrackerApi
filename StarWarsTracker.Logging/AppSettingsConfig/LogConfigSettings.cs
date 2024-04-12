using StarWarsTracker.Domain.Enums;

namespace StarWarsTracker.Logging.AppSettingsConfig
{
    public class LogConfigSettings : Dictionary<string, LogConfigCategory>
    {
        public Dictionary<string, Dictionary<string, Dictionary<string, LogLevel>>> GetConfigs() =>
            this.ToDictionary(_ => _.Key, _ => _.Value.ToLogLevelCategories());
    }
}
