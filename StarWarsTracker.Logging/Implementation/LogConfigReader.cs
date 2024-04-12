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

        /// <summary>
        /// Return the current LoggingConfigs that are being used.
        /// </summary>
        /// <returns>Dictionary of the current Logging Config Categories/Sections/Keys:Values being used.</returns>
        public Dictionary<string, Dictionary<string, Dictionary<string, LogLevel>>> GetActiveConfigs() => _activeConfigs;

        /// <summary>
        /// Get the Dictionary of Config Category/Section:Key using the ConfigCategory provided.
        /// </summary>
        /// <param name="configCategory">The Name/Key for the ConfigCategory that is being retrieved.</param>
        /// <returns></returns>
        public Dictionary<string, Dictionary<string, LogLevel>>? GetConfigCategory(string configCategory) => 
            _activeConfigs.TryGetValue(configCategory, out var category) ? category : null;

        /// <summary>
        /// Get the Dictionary of ConfigKeys:Values using the ConfigCategory/ConfigSection provided.
        /// </summary>
        /// <param name="configCategory">The Name/Key for the ConfigCategory that the ConfigSection being retrieved belongs to.</param>
        /// <param name="configSection">The Name/Key for the ConfigSection that is being retrieved. </param>
        /// <returns>Dictionary of ConfigKey:Values belonging to the Config Category/Section provided. </returns>
        public Dictionary<string, LogLevel>? GetConfigSection(string configCategory, string configSection) =>
            GetConfigCategory(configCategory)?.TryGetValue(configSection, out var section) ?? false ? section : null;

        /// <summary>
        /// Get the LogLevel belonging to the LogConfig Category CustomLogLevels[configSection[configKeyName]]
        /// </summary>
        /// <param name="configSection">The ConfigSection under CustomLogLevels Category to retrieve a LogLevel from.</param>
        /// <param name="configKeyName">The ConfigKey under the ConfigSection that the LogLevel being retrieved belongs to.</param>
        /// <returns>Returns CustomLogLevel found using the configSection and configKeyName provided.</returns>
        public LogLevel? GetCustomLogLevel(string configSection, string configKeyName) =>
            GetConfigSection(Category.CustomLogLevels, configSection)?.TryGetValue(configKeyName, out var level) ?? false ? level : null;

        /// <summary>
        /// Try to set the EndpointConfig overrides to use from the LogConfig. Return true if any overrides are set, else false if no overrides were found.
        /// </summary>
        /// <param name="endpoint">The endpoint to look for configuration overrides from the LogConfig for.</param>
        /// <returns>True if overrides are set, False if no overrides are found.</returns>
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
