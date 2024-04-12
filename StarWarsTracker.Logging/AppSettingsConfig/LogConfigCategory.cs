using StarWarsTracker.Domain.Enums;

namespace StarWarsTracker.Logging.AppSettingsConfig
{
    public class LogConfigCategory : Dictionary<string, LogConfigSection>
    {
        public Dictionary<string, Dictionary<string, LogLevel>> ToLogLevelCategories() => 
            this.ToDictionary(_ => _.Key, _ => _.Value.ToLogLevelDictionary());
    }
}
