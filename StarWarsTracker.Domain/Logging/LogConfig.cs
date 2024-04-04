using StarWarsTracker.Domain.Constants;
using StarWarsTracker.Domain.Enums;

namespace StarWarsTracker.Domain.Logging
{
    public class LogConfig : ILogConfig
    {
        private readonly Dictionary<string, Dictionary<string, Dictionary<string, LogLevel>>> _logConfigs;

        private Dictionary<string, Dictionary<string, LogLevel>> _activeConfigs;

        public LogConfig(Dictionary<string, Dictionary<string, Dictionary<string, string>>> loggingSettings)
        {
            var dictionary = new Dictionary<string, Dictionary<string, Dictionary<string, LogLevel>>>();

            foreach (var setting in loggingSettings)
            {
                dictionary.Add(setting.Key, ParseLoggingSettings(setting.Value));
            }

            _logConfigs = dictionary;

            _activeConfigs = _logConfigs[LogConfigSection.Default];
        }

        public void SetEndpointConfigs(string endpoint)
        {
            if (_logConfigs.TryGetValue(endpoint.TrimStart('/'), out var endpointConfigs))
            {
                foreach (var sections in endpointConfigs)
                {
                    if (_activeConfigs.TryGetValue(sections.Key, out var activeSection))
                    {
                        foreach (var config in sections.Value)
                        {
                            if (activeSection.ContainsKey(config.Key))
                            {
                                activeSection[config.Key] = config.Value;
                            }
                            else
                            {
                                activeSection.Add(config.Key, config.Value);
                            }
                        }
                    }
                    else
                    {
                        _activeConfigs.Add(sections.Key, sections.Value);
                    }
                }
            }
        }



        public LogLevel GetLogLevel(string section, string configKeyName) => _activeConfigs[section][configKeyName];

        private LogLevel ParseLogLevel(string configKey, string logLevel) => 
            Enum.TryParse<LogLevel>(logLevel, out var level) ? level : throw new ApplicationException($"Invalid Log Level: {configKey} - Value: {logLevel}");

        private Dictionary<string, Dictionary<string, LogLevel>> ParseLoggingSettings(Dictionary<string, Dictionary<string, string>> logSettings)
        {
            var settings = new Dictionary<string, Dictionary<string, LogLevel>>();

            foreach (var section in logSettings)
            {
                var configs = new Dictionary<string, LogLevel>();

                foreach (var config in section.Value)
                {
                    configs.Add(config.Key, ParseLogLevel(config.Key, config.Value));
                }

                settings.Add(section.Key, configs);
            }

            return settings;
        }
    }
}
