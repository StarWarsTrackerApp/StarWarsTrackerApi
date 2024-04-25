using StarWarsTracker.Domain.Constants.LogConfigs;
using StarWarsTracker.Domain.Exceptions;
using StarWarsTracker.Logging.Abstraction;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace StarWarsTracker.Api.Middleware
{
    /// <summary>
    /// This class is responsible for acting as a global exception handler in the middleware.
    /// This ensures that custom exceptions are returned with consistent response bodies and expected status codes.
    /// Other unhandled exceptions are treated as 500 Internal Server Error.
    /// Logging is enabled based on Logging Configurations from appsettings.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        #region Private Members

        private readonly IClassLogger _logger;

        private readonly ILogConfigReader _logConfigReader;

        #endregion

        #region Constructor

        public ExceptionHandlingMiddleware(IClassLoggerFactory loggerFactory, ILogConfigReader logConfigReader)
        {
            _logger = loggerFactory.GetLoggerFor(this);
            _logConfigReader = logConfigReader;
        }

        #endregion

        #region Public IMiddleware Method

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            context.Response.ContentType = "application/json";

            try
            {
                await next(context);
            }
            catch (CustomException e)
            {
                var response = e.GetResponseBody();

                var logLevel = _logConfigReader.GetLogLevel(Section.ExceptionLogging, e.GetLogLevelConfigKey()) ?? Domain.Enums.LogLevel.None;

                _logger.IncreaseLevel(logLevel, e.GetType().Name, response);

                context.Response.StatusCode = e.GetStatusCode();

                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
            catch (Exception e)
            {
                var logLevel = _logConfigReader.GetLogLevel(Section.ExceptionLogging, Key.DefaultExceptionLogLevel) ?? Domain.Enums.LogLevel.None;

                _logger.IncreaseLevel(logLevel, "Exception Caught", new { e.GetType().Name, e.Message, e.StackTrace });
                
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                await context.Response.WriteAsync("Unexpected Error.");
            }
        }
        
        #endregion
    }
}
