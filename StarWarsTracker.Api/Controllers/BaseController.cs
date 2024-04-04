using StarWarsTracker.Domain.Constants;
using StarWarsTracker.Domain.Logging;
using System.Runtime.CompilerServices;

namespace StarWarsTracker.Api.Controllers
{
    public abstract class BaseController
    {
        private readonly IOrchestrator _orchestrator;

        private readonly ILogMessage _logMessage;

        public BaseController(IOrchestrator orchestrator, ILogMessage logMessage)
        {
            _orchestrator = orchestrator;

            _logMessage = logMessage;
        }

        public async Task ExecuteRequestAsync(IRequest request, [CallerMemberName] string methodCalling = "")
        {
            _logMessage.AddConfiguredLogLevel(LogConfigSection.CustomLogLevels, LogConfigKey.ControllerRequestBodyLogLevel, this, "Request Received", request, methodCalling);

            await _orchestrator.ExecuteRequestAsync(request);

            _logMessage.AddConfiguredLogLevel(LogConfigSection.CustomLogLevels, LogConfigKey.ControllerResponseBodyLogLevel, this, "Request Handled", methodCalling: methodCalling);
        }

        public async Task<TResponse> GetResponseAsync<TResponse>(IRequestResponse<TResponse> request, [CallerMemberName] string methodCalling = "")
        {
            _logMessage.AddConfiguredLogLevel(LogConfigSection.CustomLogLevels, LogConfigKey.ControllerRequestBodyLogLevel, this, "Request Received", request, methodCalling);

            var response = await _orchestrator.GetRequestResponseAsync(request);

            _logMessage.AddConfiguredLogLevel(LogConfigSection.CustomLogLevels, LogConfigKey.ControllerResponseBodyLogLevel, this, "Response Received", response, methodCalling);

            return response;
        }
    }
}
