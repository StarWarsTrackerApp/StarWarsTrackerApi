using Microsoft.Extensions.DependencyInjection;
using StarWarsTracker.Logging.Abstraction;

namespace StarWarsTracker.Application.Implementation
{
    internal class TypeActivator : ITypeActivator
    {
        private readonly IServiceProvider _serviceProvider;

        private readonly IClassLogger _logger;

        public TypeActivator(IServiceProvider serviceProvider, IClassLoggerFactory loggerFactory)
        {
            _serviceProvider = serviceProvider;

            _logger = loggerFactory.GetLoggerFor(this);
        }

        public TResponse Instantiate<TResponse>(Type typeToInstantiate)
        {
            _logger.AddTrace("Attempting to instantiate", typeToInstantiate.Name);

            try
            {
                var obj = ActivatorUtilities.CreateInstance(_serviceProvider, typeToInstantiate);

                if (obj is TResponse response)
                {
                    _logger.AddTrace("Instantiated Object", response);

                    return response;
                }

                _logger.IncreaseLevel(LogLevel.Critical, $"Instantiated Object Not Type of TResponse {typeof(TResponse).Name}", obj.GetType().Name);

                throw new OperationFailedException();

            }
            catch (Exception e)
            {
                _logger.IncreaseLevel(LogLevel.Critical, $"Exception Thrown When Instantiating Object {typeToInstantiate.Name}", new { e.GetType().Name, e.Message, e.StackTrace });

                throw;
            }
        }
    }
}
