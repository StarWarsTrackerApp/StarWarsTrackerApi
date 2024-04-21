using StarWarsTracker.Domain.Constants.LogConfigs;
using StarWarsTracker.Domain.Enums;
using StarWarsTracker.Logging.Abstraction;

namespace StarWarsTracker.Logging.Implementation
{
    internal class LogConfigReader : ILogConfigReader
    {
        #region Private Members

        private readonly ILogConfig _logConfig;

        private Dictionary<string, Dictionary<string, Dictionary<string, LogLevel>>> _activeConfigs;

        #endregion

        #region Constructor

        public LogConfigReader(ILogConfig logConfig)
        {
            _logConfig = logConfig;
            _activeConfigs = _logConfig.GetDefaultConfigs();
        }

        #endregion

        #region Public ILogConfigReader Methods

        public Dictionary<string, Dictionary<string, Dictionary<string, LogLevel>>> GetActiveConfigs() => _activeConfigs;

        public Dictionary<string, Dictionary<string, LogLevel>>? GetConfigCategory(string configCategory) => 
            _activeConfigs.TryGetValue(configCategory, out var category) ? category : null;

        public Dictionary<string, LogLevel>? GetConfigSection(string configCategory, string configSection) =>
            GetConfigCategory(configCategory)?.TryGetValue(configSection, out var section) ?? false ? section : null;

        public LogLevel? GetCustomLogLevel(string configSection, string configKeyName) =>
            GetConfigSection(Category.CustomLogLevels, configSection)?.TryGetValue(configKeyName, out var level) ?? false ? level : null;

        public bool TrySetEndpointConfigs(string endpoint)
        {
            var endpointConfigs = _logConfig.GetEndpointConfigs(endpoint.TrimStart('/'));

            if (endpointConfigs == null)
            {
                return false;
            }

            foreach ( var endpointConfigCategory in endpointConfigs )
            {
                if (_activeConfigs.TryGetValue(endpointConfigCategory.Key, out var activeCategory))
                {
                    foreach (var endpointConfigSection in endpointConfigCategory.Value)
                    {
                        if (activeCategory.TryGetValue(endpointConfigSection.Key, out var activeSection))
                        {
                            foreach (var endpointConfig in endpointConfigSection.Value)
                            {
                                if (activeSection.ContainsKey(endpointConfig.Key))
                                {
                                    activeSection[endpointConfig.Key] = endpointConfig.Value;
                                }
                                else
                                {
                                    activeSection.Add(endpointConfig.Key, endpointConfig.Value);
                                }
                            }
                        }
                        else
                        {
                            activeCategory.Add(endpointConfigSection.Key, endpointConfigSection.Value);
                        }
                    }
                }
                else
                {
                    _activeConfigs.Add(endpointConfigCategory.Key, endpointConfigCategory.Value);
                }
            }

            return true;
        }

        #endregion
    }
}
