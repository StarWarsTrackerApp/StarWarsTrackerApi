using Microsoft.Extensions.DependencyInjection;
using StarWarsTracker.Application.Implementation;
using StarWarsTracker.Logging.Abstraction;
using System.Diagnostics.CodeAnalysis;

namespace StarWarsTracker.Application
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

            services.AddScoped<ILogWriter, DatabaseLogger>();

            services.AddSingleton(HandlerDictionary.FromDictionaryAssembly());

            services.AddScoped<IHandlerFactory, HandlerFactory>();

            services.AddTransient<IServiceProvider>(_ => services.BuildServiceProvider());

            services.AddTransient<ITypeActivator, TypeActivator>();

            return services;
        }
    }
}
