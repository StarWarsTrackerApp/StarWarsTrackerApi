using Microsoft.Extensions.DependencyInjection;
using StarWarsTracker.Application.Implementation;
using StarWarsTracker.Persistence.Implementation;
using StarWarsTracker.Tests.Shared;

namespace StarWarsTracker.Api.Tests.IntegrationTests
{
    /// <summary>
    /// This base class for Controller Tests can be re-used for initializing a Controller to test API Endpoints with
    /// </summary>
    /// <typeparam name="TController"></typeparam>
    public abstract class ControllerTest<TController> where TController : BaseController
    {
        // using static IServiceCollection so that we do not re-initialize singleton/scoped dependencies for each test.
        private static readonly IServiceCollection _services;
        
        // Static constructor to be called once for setting the _services IServiceCollection that will be shared across all Controller Tests
        static ControllerTest()
        {
            _services = new ServiceCollection();

            // Inject Dependencies
            _services.InjectPersistenceDependencies($"Data Source={Hidden.DbServer};Initial Catalog={Hidden.DbName};Integrated Security=True;");
            _services.InjectApplicationDependencies();
        }

        // Instantiate the controller for each test class using the IServiceCollection similar to how the API will do when an endpoint is called.
        protected readonly TController _controller = (TController)ActivatorUtilities.CreateInstance(_services.BuildServiceProvider(), typeof(TController));
    }
}
