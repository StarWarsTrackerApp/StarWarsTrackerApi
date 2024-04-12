using Microsoft.Extensions.DependencyInjection;
using StarWarsTracker.Logging.Abstraction;
using StarWarsTracker.Logging.AppSettingsConfig;
using StarWarsTracker.Logging.Implementation;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("StarWarsTracker.Logging.Tests")]
namespace StarWarsTracker.Logging
{
    public static class DependencyInjection
    {
        public static IServiceCollection InjectLoggingDependencies(this IServiceCollection services, Dictionary<string, LogConfigSettings> configSettings)
        {
            if (services == null)
            {
                throw new NullReferenceException(nameof(services));
            }

            services.AddSingleton<ILogConfig>(new LogConfig(configSettings));

            services.AddScoped<ILogConfigReader, LogConfigReader>();
            services.AddScoped<IClassLoggerFactory, ClassLoggerFactory>();
            services.AddScoped<ILogMessage, LogMessage>();

            return services;
        }
    }
}
