using Microsoft.Extensions.DependencyInjection;
using StarWarsTracker.Domain.Logging;

namespace StarWarsTracker.Application.Implementation
{
    internal class TypeActivator : ITypeActivator
    {
        private readonly IServiceProvider _serviceProvider;

        private readonly ILogMessage _logMessage;

        public TypeActivator(IServiceProvider serviceProvider, ILogMessage logMessage)
        {
            _serviceProvider = serviceProvider;

            _logMessage = logMessage;
        }

        public TResponse Instantiate<TResponse>(Type typeToInstantiate)
        {
            _logMessage.AddTrace(this, "Attempting to instantiate", typeToInstantiate.Name);

            try
            {
                var obj = ActivatorUtilities.CreateInstance(_serviceProvider, typeToInstantiate);

                if (obj is TResponse response)
                {
                    _logMessage.AddTrace(this, "Instantiated Object", obj.GetType().Name);

                    return response;
                }

                _logMessage.IncreaseLevel(LogLevel.Critical, this, $"Instantiated Object Not Type of TResponse {typeof(TResponse).Name}", obj.GetType().Name);

                throw new OperationFailedException();

            }
            catch (Exception e)
            {
                _logMessage.IncreaseLevel(LogLevel.Critical, this, $"Exception Thrown When Instantiating Object {typeToInstantiate.Name}", new { e.GetType().Name, e.Message, e.StackTrace });

                throw;
            }
        }
    }
}
