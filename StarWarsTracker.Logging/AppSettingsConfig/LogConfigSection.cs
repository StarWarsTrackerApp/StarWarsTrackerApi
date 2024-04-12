using StarWarsTracker.Domain.Enums;

namespace StarWarsTracker.Logging.AppSettingsConfig
{
    public class LogConfigSection : Dictionary<string, string>
    {
        public Dictionary<string, LogLevel> ToLogLevelDictionary()
        {
            var dictionary = new Dictionary<string, LogLevel>();

            foreach (var item in this)
            {
                if (Enum.TryParse<LogLevel>(item.Value, ignoreCase: true, out var logLevel))
                {
                    dictionary.Add(item.Key, logLevel);
                }
                else
                {
                    throw new ApplicationException($"Invalid LogLevel Config - Key: {item.Key} - Value: {item.Value}");
                }
            }

            return dictionary;
        }
    }
}
