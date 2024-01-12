using Microsoft.Extensions.DependencyInjection;

namespace StarWarsTracker.Persistence.Implementation
{
    public static class DependencyInjection
    {
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
