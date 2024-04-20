using StarWarsTracker.Domain.Enums;

namespace StarWarsTracker.Logging.Abstraction
{
    /// <summary>
    /// This interface defines the contract for the LogConfig that will be stored/saved at startup. 
    /// This is used by the ILogConfigReader to get Default/Endpoint Specific logging configurations.
    /// </summary>
    internal interface ILogConfig
    {
        /// <summary>
        /// Return a copy of the Default LoggingConfigs.
        /// </summary>
        /// <returns>Copy of the Default Configs. </returns>
        Dictionary<string, Dictionary<string, Dictionary<string, LogLevel>>> GetDefaultConfigs();

        /// <summary>
        /// Use the endpoint provided to locate the config overrides for that endpoint and return a copy of them.
        /// </summary>
        /// <param name="endpointName">The name of the endpoint to locate Configs for.</param>
        /// <returns>Returns a copy of the Dictionary of ConfigCategories/Sections/Keys:Values overrides for the endpoint</returns>
        Dictionary<string, Dictionary<string, Dictionary<string, LogLevel>>>? GetEndpointConfigs(string endpointName);
    }
}
