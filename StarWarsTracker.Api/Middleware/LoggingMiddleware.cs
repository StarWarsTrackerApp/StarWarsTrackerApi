using StarWarsTracker.Logging.Abstraction;

namespace StarWarsTracker.Api.Middleware
{
    public class LoggingMiddleware : IMiddleware
    {
        private readonly ILogWriter _logWriter;

        private readonly IClassLogger _logger;

        private readonly ILogMessage _logMessage;

        private readonly ILogConfigReader _logConfigReader;

        public LoggingMiddleware(ILogWriter logWriter, ILogMessage logMessage, IClassLoggerFactory loggerFactory, ILogConfigReader logConfigReader)
        {
            _logWriter = logWriter;
            _logger = loggerFactory.GetLoggerFor(this);
            _logConfigReader = logConfigReader;
            _logMessage = logMessage;
        }

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
    }
}
