using Microsoft.Extensions.DependencyInjection;
using StarWarsTracker.Application.Abstraction;

namespace StarWarsTracker.Application.Implementation
{
    internal class TypeActivator : ITypeActivator
    {
        private readonly IServiceProvider _serviceProvider;

        public TypeActivator(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

        public TResponse Instantiate<TResponse>(Type typeToInstantiate) => (TResponse)ActivatorUtilities.CreateInstance(_serviceProvider, typeToInstantiate);
    }
}
