using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace StarWarsTracker.Persistence.Implementation
{
    [ExcludeFromCodeCoverage]
    public static class DependencyInjection
    {
        [ExcludeFromCodeCoverage]
        public static IServiceCollection InjectPersistenceDependencies(this IServiceCollection services, string connectionString)
        {
            if (services == null)
            {
                throw new NullReferenceException(nameof(services));
            }

            services.AddSingleton<IDbConnectionFactory>(new SqlConnectionFactory(connectionString));

            services.AddSingleton<IDataAccess, DataAccess>();

            return services;
        }
    }
}
