using Microsoft.Extensions.DependencyInjection;
using StarWarsTracker.ApiCaller.Abstraction;

namespace StarWarsTracker.ApiCaller.Implementation
{
    public static class DependencyInjection
    {
        public static IServiceCollection InjectApiCallerDependencies(this IServiceCollection services, string starWarsTrackerApiBaseUrl)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddSingleton<IHttpRequestBuilder, HttpRequestBuilder>();

            services.AddSingleton<IApiService, ApiService>();

            services.AddSingleton(new StarWarsTrackerApiUrl(starWarsTrackerApiBaseUrl));

            services.AddSingleton<IApiCaller, StarWarsTrackerApiCaller>();

            return services;
        }
    }
}
