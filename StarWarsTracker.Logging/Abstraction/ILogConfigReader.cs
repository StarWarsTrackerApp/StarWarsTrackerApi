using StarWarsTracker.Domain.Enums;

namespace StarWarsTracker.Logging.Abstraction
{
    /// <summary>
    /// This interface defines the contract for how a class is able to obtain LogLevel Configurations from the LogConfig
    /// </summary>
    public interface ILogConfigReader
    {
        /// <summary>
        /// Try to set the EndpointConfig overrides to use from the LogConfig. Return true if any overrides are set, else false if no overrides were found.
        /// </summary>
        /// <param name="endpoint">The endpoint to look for configuration overrides from the LogConfig for.</param>
        /// <returns>True if overrides are set, False if no overrides are found.</returns>
        public bool TrySetEndpointConfigs(string endpoint);

        /// <summary>
        /// Get the Dictionary of Config Category/Section:Key using the ConfigCategory provided.
        /// </summary>
        /// <param name="configCategory">The Name/Key for the ConfigCategory that is being retrieved.</param>
        /// <returns>Dictionary where the Key is ConfigSectionNames and the Value is a Dictionary of ConfigKeys/LogLevelValues</returns>
        public Dictionary<string, Dictionary<string, LogLevel>>? GetConfigCategory(string configCategory);

        /// <summary>
        /// Get the Dictionary of ConfigKeys:Values using the ConfigCategory/ConfigSection provided.
        /// </summary>
        /// <param name="configCategory">The Name/Key for the ConfigCategory that the ConfigSection being retrieved belongs to.</param>
        /// <param name="configSection">The Name/Key for the ConfigSection that is being retrieved. </param>
        /// <returns>Dictionary of ConfigKey:Values belonging to the Config Category/Section provided. </returns>
        public Dictionary<string, LogLevel>? GetConfigSection(string configCategory, string configSection);

        /// <summary>
        /// Get the LogLevel belonging to the LogConfig Category CustomLogLevels[configSection[configKeyName]]
        /// </summary>
        /// <param name="configSection">The ConfigSection under CustomLogLevels Category to retrieve a LogLevel from.</param>
        /// <param name="configKeyName">The ConfigKey under the ConfigSection that the LogLevel being retrieved belongs to.</param>
        /// <returns>CustomLogLevel found using the configSection and configKeyName provided.</returns>
        public LogLevel? GetCustomLogLevel(string configSection, string configKeyName);

        /// <summary>
        /// Return the current LoggingConfigs that are being used.
        /// </summary>
        /// <returns>Dictionary of the current Logging Config Categories/Sections/Keys:Values being used.</returns>
        public Dictionary< string, Dictionary<string, Dictionary<string, LogLevel>>> GetActiveConfigs();
    }
}
