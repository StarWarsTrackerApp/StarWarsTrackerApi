using StarWarsTracker.Domain.Constants.LogConfigs;
using StarWarsTracker.Domain.Exceptions;
using StarWarsTracker.Logging.Abstraction;
using System.Runtime.CompilerServices;

namespace StarWarsTracker.Api.Controllers
{
    public abstract class BaseController
    {
        #region Private Members

        private readonly IOrchestrator _orchestrator;

        private readonly IClassLogger _logger;

        #endregion

        #region Constructor

        public BaseController(IOrchestrator orchestrator, IClassLoggerFactory loggerFactory)
        {
            _orchestrator = orchestrator;

            _logger = loggerFactory.GetLoggerFor(this);
        }

        #endregion

        #region Public Methods

        public async Task ExecuteRequestAsync(IRequest request, [CallerMemberName] string methodCalling = "")
        {
            if (request == null)
            {
                _logger.AddDebug("Null Request Received", typeof(IRequest).Name);
                throw new ValidationFailureException("Null Request Received");
            }

            LogRequest(request, methodCalling);

            await _orchestrator.ExecuteRequestAsync(request);

            LogResponse(null, methodCalling);
        }

        public async Task<TResponse> GetResponseAsync<TResponse>(IRequestResponse<TResponse> request, [CallerMemberName] string methodCalling = "")
        {
            if (request == null)
            {
                _logger.AddDebug("Null Request Received", typeof(IRequest).Name);
                throw new ValidationFailureException("Null Request Received");
            }

            LogRequest(request, methodCalling);

            var response = await _orchestrator.GetRequestResponseAsync(request);

            LogResponse(response, methodCalling);

            return response;
        }

        #endregion

        #region Private Helpers For Logging

        private void LogRequest(object request, string methodCalling) => 
            _logger.AddConfiguredLogLevel(Section.ControllerLogging, Key.ControllerRequestBodyLogLevel, "Request Received", request, methodCalling);

        private void LogResponse(object? response, string methodCalling) =>
            _logger.AddConfiguredLogLevel(Section.ControllerLogging, Key.ControllerResponseBodyLogLevel, "Response Received", response, methodCalling);
        
        #endregion
    }
}
