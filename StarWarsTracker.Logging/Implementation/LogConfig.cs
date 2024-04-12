using StarWarsTracker.Domain.Constants.LogConfigs;
using StarWarsTracker.Domain.Enums;
using StarWarsTracker.Logging.Abstraction;
using StarWarsTracker.Logging.AppSettingsConfig;

namespace StarWarsTracker.Logging.Implementation
{
    internal class LogConfig : ILogConfig
    {
        #region Private Members

        private readonly Dictionary<string, Dictionary<string, Dictionary<string, LogLevel>>> _defaultConfigs;

        private readonly Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, LogLevel>>>> _endpointConfigs;

        #endregion
        #region Constructor

        public LogConfig(Dictionary<string, LogConfigSettings> logConfigSettings)
        {
            _endpointConfigs = logConfigSettings.ToDictionary(_ => _.Key, _ => _.Value.GetConfigs());

            _defaultConfigs = _endpointConfigs[Category.Default];
        }

        #endregion

        #region Public ILogConfig Properties

        /// <summary>
        /// Return a copy of the Default LoggingConfigs.
        /// </summary>
        /// <returns>Copy of the Default Configs. </returns>
        public Dictionary<string, Dictionary<string, Dictionary<string, LogLevel>>> GetDefaultConfigs() => Copy(_defaultConfigs);

        /// <summary>
        /// Use the endpoint provided to locate the config overrides for that endpoint and return a copy of them.
        /// </summary>
        /// <param name="endpointName">The name of the endpoint to locate Configs for.</param>
        /// <returns>Returns a copy of the Dictionary of ConfigCategories/Sections/Keys:Values overrides for the endpoint</returns>
        public Dictionary<string, Dictionary<string, Dictionary<string, LogLevel>>>? GetEndpointConfigs(string endpointName)
        {
            if (string.IsNullOrWhiteSpace(endpointName))
            {
                return null;
            }

            if(_endpointConfigs.TryGetValue(endpointName, out var configs))
            {
                return Copy(configs);
            }

            return null;
        }

        #endregion

        #region Private Helper Methods

        /// <summary>
        /// Helper to copy a Config so that if a consumer changes a dictionary, the source is not modified.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static Dictionary<string, Dictionary<string, Dictionary<string, LogLevel>>> Copy(Dictionary<string, Dictionary<string, Dictionary<string, LogLevel>>> input) =>
            input.ToDictionary(c => c.Key, c => c.Value.ToDictionary(s => s.Key, s => s.Value.ToDictionary(_ => _.Key, _ => _.Value)));
        
        #endregion
    }
}
