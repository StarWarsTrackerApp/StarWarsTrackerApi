using StarWarsTracker.Domain.Enums;

namespace StarWarsTracker.Logging.AppSettingsConfig
{
    /// <summary>
    /// LogConfigCategory represents the Dictionary (of String, LogConfigSection) that 
    /// contains ConfigSections where each Section is a Dictionary of string, LogLevel.
    /// </summary>
    public class LogConfigCategory : Dictionary<string, LogConfigSection>
    {
        public Dictionary<string, Dictionary<string, LogLevel>> ToLogLevelCategories() => 
            this.ToDictionary(_ => _.Key, _ => _.Value.ToLogLevelDictionary());
    }
}
