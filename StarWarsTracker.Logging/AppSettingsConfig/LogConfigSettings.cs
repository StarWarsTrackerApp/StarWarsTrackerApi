using StarWarsTracker.Domain.Enums;

namespace StarWarsTracker.Logging.AppSettingsConfig
{
    /// <summary>
    /// LogConfigSettings represents the Dictionary (of String, LogConfigCategory) that contains the Default or Endpoint Override Configurations.
    /// Each LogConfigCategory is a Dictionary (of String, LogConfigSection) that contains many Sections.
    /// Each LogConfigSection is a Dictionary (of String, LogLevel) that contains many LogLevels.
    /// </summary>
    public class LogConfigSettings : Dictionary<string, LogConfigCategory>
    {
        public Dictionary<string, Dictionary<string, Dictionary<string, LogLevel>>> GetConfigs() =>
            this.ToDictionary(_ => _.Key, _ => _.Value.ToLogLevelCategories());
    }
}
