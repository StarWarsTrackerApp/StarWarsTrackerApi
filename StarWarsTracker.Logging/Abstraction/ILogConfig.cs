using StarWarsTracker.Domain.Enums;

namespace StarWarsTracker.Logging.Abstraction
{
    internal interface ILogConfig
    {
        Dictionary<string, Dictionary<string, Dictionary<string, LogLevel>>> GetDefaultConfigs();

        Dictionary<string, Dictionary<string, Dictionary<string, LogLevel>>>? GetEndpointConfigs(string endpointName);
    }
}
