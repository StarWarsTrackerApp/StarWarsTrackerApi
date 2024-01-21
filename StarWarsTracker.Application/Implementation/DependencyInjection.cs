using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace StarWarsTracker.Application.Implementation
{
    [ExcludeFromCodeCoverage]
    public static class DependencyInjection
    {
        public static IServiceCollection InjectApplicationDependencies(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new NullReferenceException(nameof(services));
            }

            services.AddSingleton<IHandlerDictionary>(new HandlerDictionary());

            services.AddSingleton<IHandlerFactory, HandlerFactory>();

            services.AddSingleton<IOrchestrator, Orchestrator>();

            services.AddTransient<IServiceProvider>(_ => services.BuildServiceProvider());

            services.AddTransient<ITypeActivator, TypeActivator>();

            return services;
        }
    }
}
