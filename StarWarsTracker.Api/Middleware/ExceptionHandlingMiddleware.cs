using StarWarsTracker.Domain.Constants;
using StarWarsTracker.Domain.Exceptions;
using StarWarsTracker.Domain.Logging;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace StarWarsTracker.Api.Middleware
{
    [ExcludeFromCodeCoverage]
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogMessage _logMessage;

        private readonly ILogConfig _logConfig;

        public ExceptionHandlingMiddleware(ILogMessage logMessage, ILogConfig logConfig)
        {
            _logMessage = logMessage;
            _logConfig = logConfig;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            context.Response.ContentType = "application/json";

            try
            {
                await next(context);
            }
            catch (ValidationFailureException e)
            {
                _logMessage.IncreaseLevel(_logConfig.GetLogLevel(LogConfigSection.ExceptionLogging, LogConfigKey.ValidationFailureExceptionLogLevel), this, "ValidationFailureException Caught",
                    new { e.GetType().Name, e.ValidationFailureMessages, e.StackTrace });

                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                await context.Response.WriteAsync(JsonSerializer.Serialize(e.ValidationFailureMessages));
            }
            catch (DoesNotExistException e)
            {
                _logMessage.IncreaseLevel(_logConfig.GetLogLevel(LogConfigSection.ExceptionLogging, LogConfigKey.DoesNotExistExceptionLogLevel), this, "DoesNotExistException Caught",
                    new { e.GetType().Name, e.NameOfObjectNotExisting, e.ValuesSearchedBy, e.StackTrace });

                context.Response.StatusCode = StatusCodes.Status404NotFound;

                await context.Response.WriteAsync(JsonSerializer.Serialize(new { e.NameOfObjectNotExisting, e.ValuesSearchedBy }));
            }
            catch (AlreadyExistsException e)
            {
                _logMessage.IncreaseLevel(_logConfig.GetLogLevel(LogConfigSection.ExceptionLogging, LogConfigKey.AlreadyExistsExceptionLogLevel), this, "AlreadyExistsException Caught",
                    new { e.GetType().Name, e.NameOfObjectAlreadyExisting, e.Conflicts, e.StackTrace });

                context.Response.StatusCode = StatusCodes.Status409Conflict;

                await context.Response.WriteAsync(JsonSerializer.Serialize(new { e.Conflicts }));
            }
            catch (Exception e)
            {
                _logMessage.IncreaseLevel(_logConfig.GetLogLevel(LogConfigSection.ExceptionLogging, LogConfigKey.DefaultExceptionLogLevel), this, $"Exception Caught", 
                    new { e.GetType().Name, e.Message, e.StackTrace });
                
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                await context.Response.WriteAsync("Unexpected Error.");
            }
        }
    }
}
