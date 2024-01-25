using StarWarsTracker.Domain.Exceptions;
using System.Text.Json;

namespace StarWarsTracker.Api.Middleware
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                context.Response.ContentType = "application/json";

                context.Response.StatusCode = GetStatusCode(exception);

                await context.Response.WriteAsync(GetContent(exception));
            }
        }

        private static int GetStatusCode(Exception exception) => exception switch
        {
            ValidationFailureException => StatusCodes.Status400BadRequest,
            DoesNotExistException => StatusCodes.Status404NotFound,
            AlreadyExistsException => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError,
        };

        private static string GetContent(Exception exception) => exception switch
        {
            ValidationFailureException e => JsonSerializer.Serialize(e.ValidationFailureMessages),
            DoesNotExistException e => JsonSerializer.Serialize(new { e.NameOfObjectNotExisting, e.ValuesSearchedBy }),
            AlreadyExistsException e => JsonSerializer.Serialize(e.Conflicts),
            _ => "Unexpected Error"
        };
    }
}
