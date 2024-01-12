using Microsoft.Extensions.DependencyInjection;
using StarWarsTracker.Application.Abstraction;

namespace StarWarsTracker.Application.Implementation
{
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

            return services;
        }
    }
}
