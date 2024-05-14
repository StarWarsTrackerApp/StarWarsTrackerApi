using System.Diagnostics.CodeAnalysis;

namespace StarWarsTracker.Domain.Constants
{
    /// <summary>
    /// The constants in this class are used to map to sections/values in the appsettings.json
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class AppConfigKeys
    {
        /// <summary>
        /// The key for the Default ConnectionString for the Database.
        /// </summary>
        public static string DefaultConnectionString = "Default";

        /// <summary>
        /// The key for the LoggingConfigurations
        /// </summary>
        public static string LoggingConfigurations = "Logging:Environment:";
    }
}
