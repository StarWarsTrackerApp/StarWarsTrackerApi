using Microsoft.Extensions.DependencyInjection;
using StarWarsTracker.Domain.Logging;
using System.Diagnostics.CodeAnalysis;

namespace StarWarsTracker.Application.Implementation
{
    [ExcludeFromCodeCoverage]
    public static class DependencyInjection
    {
        public static IServiceCollection InjectApplicationDependencies(this IServiceCollection services, Dictionary<string, Dictionary<string, Dictionary<string, string>>> logSettings)
        {
            if (services == null)
            {
                throw new NullReferenceException(nameof(services));
            }

            services.AddSingleton(logSettings);
            services.AddScoped<ILogConfig, LogConfig>();
            services.AddScoped<ILogger, DatabaseLogger>();
            services.AddScoped<ILogMessage, LogMessage>();

            services.AddSingleton<IHandlerDictionary>(new HandlerDictionary());

            services.AddScoped<IOrchestrator, Orchestrator>();
            services.AddScoped<IHandlerFactory, HandlerFactory>();

            services.AddTransient<IServiceProvider>(_ => services.BuildServiceProvider());

            services.AddTransient<ITypeActivator, TypeActivator>();

            return services;
        }
    }
}
