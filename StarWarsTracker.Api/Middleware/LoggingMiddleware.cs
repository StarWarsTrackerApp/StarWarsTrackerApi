using StarWarsTracker.Logging.Abstraction;

namespace StarWarsTracker.Api.Middleware
{
    /// <summary>
    /// This class is responsible for acting as a global Logger in the middleware.
    /// LogConfigReaders have their EndpointOverrides set as the request comes in the pipeline.
    /// Additional Try/Catch is enabled in case of any exceptions making it past Global Exception Handler.
    /// LogMessage is saved using ILogWriter when the request is leaving the pipeline.
    /// </summary>
    public class LoggingMiddleware : IMiddleware
    {
        #region Private Members

        private readonly ILogWriter _logWriter;

        private readonly IClassLogger _logger;

        private readonly ILogMessage _logMessage;

        private readonly ILogConfigReader _logConfigReader;

        #endregion

        #region Constructor

        public LoggingMiddleware(ILogWriter logWriter, ILogMessage logMessage, IClassLoggerFactory loggerFactory, ILogConfigReader logConfigReader)
        {
            _logWriter = logWriter;
            _logger = loggerFactory.GetLoggerFor(this);
            _logConfigReader = logConfigReader;
            _logMessage = logMessage;
        }

        #endregion

        #region Public IMiddleware Method

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var requestPath = context.Request.Path;

            var method = context.Request.Method;

            try
            {
                _logger.AddTrace("Logging Started", new { RequestPath = requestPath, Method = method });

                if (_logConfigReader.TrySetEndpointConfigs(requestPath))
                {
                    _logger.AddDebug("Endpoint Logging Overrides Applied", _logConfigReader.GetActiveConfigs());
                }
                else
                {
                    _logger.AddDebug("No Endpoint Logging Overrides Found", _logConfigReader.GetActiveConfigs());
                }

                await next(context);
            }
            catch (Exception e)
            {
                _logger.IncreaseLevel(Domain.Enums.LogLevel.Critical, "Unhandled Exception Reached Logging Middleware", new { e.GetType().Name, e.Message, e.StackTrace });
                throw;
            }
            finally
            {
                _logger.AddTrace("Logging Complete");

                _logWriter.Write(_logMessage, requestPath, method);
            }
        }
        
        #endregion
    }
}
