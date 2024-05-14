using StarWarsTracker.Domain.Constants.LogConfigs;
using StarWarsTracker.Logging.Abstraction;
using System.Diagnostics.CodeAnalysis;

namespace StarWarsTracker.Api.Middleware
{
    /// <summary>
    /// This class is responsible for acting as a global exception handler in the middleware to capture any missed/unhandled exceptions.
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
            catch (Exception e)
            {
                var logLevel = _logConfigReader.GetLogLevel(Section.ExceptionLogging, Key.DefaultExceptionLogLevel) ?? Domain.Enums.LogLevel.Critical;

                _logger.IncreaseLevel(logLevel, "Exception Caught", new { e.GetType().Name, e.Message, e.StackTrace });

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                await context.Response.WriteAsync("Unexpected Error.");
            }
        }
        
        #endregion
    }
}
