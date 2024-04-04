using StarWarsTracker.Domain.Logging;

namespace StarWarsTracker.Api.Middleware
{
    public class LoggingMiddleware : IMiddleware
    {
        private readonly Domain.Logging.ILogger _logger;

        private readonly ILogMessage _logMessage;

        private readonly ILogConfig _logConfig;

        public LoggingMiddleware(Domain.Logging.ILogger logger, ILogMessage logMessage, ILogConfig logConfig)
        {
            _logger = logger;

            _logMessage = logMessage;

            _logConfig = logConfig;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var requestPath = context.Request.Path;

            var method = context.Request.Method;

            _logConfig.SetEndpointConfigs(requestPath);

            try
            {
                _logMessage.AddTrace(this, "Logging Started", new { RequestPath = requestPath, Method = method });

                await next(context);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logMessage.AddTrace(this, "Logging Complete");

                _logger.Log(_logMessage, requestPath, method);
            }
        }
    }
}
