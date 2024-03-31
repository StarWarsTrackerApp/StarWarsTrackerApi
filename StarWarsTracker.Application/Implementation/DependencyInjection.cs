using Microsoft.Extensions.DependencyInjection;
using StarWarsTracker.Domain.Logging;
using System.Diagnostics.CodeAnalysis;

namespace StarWarsTracker.Application.Implementation
{
    [ExcludeFromCodeCoverage]
    public static class DependencyInjection
    {
        public static IServiceCollection InjectApplicationDependencies(this IServiceCollection services, string nameOfLogLevel)
        {
            if (services == null)
            {
                throw new NullReferenceException(nameof(services));
            }

            if (Enum.TryParse<LogLevel>(nameOfLogLevel, out var logLevel))
            {
                services.AddSingleton(new LoggerFactory.LogSettings(logLevel));
                services.AddSingleton<ILoggerFactory, LoggerFactory>();
            }
            else
            {
                throw new InvalidDataException(nameof(nameOfLogLevel));
            }

            services.AddSingleton<IHandlerDictionary>(new HandlerDictionary());

            services.AddScoped<IOrchestrator, Orchestrator>();

            services.AddScoped<IHandlerFactory, HandlerFactory>();

            services.AddTransient<IServiceProvider>(_ => services.BuildServiceProvider());

            services.AddTransient<ITypeActivator, TypeActivator>();

            return services;
        }
    }
}
