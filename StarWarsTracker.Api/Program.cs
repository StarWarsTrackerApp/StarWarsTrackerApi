using StarWarsTracker.Api.Middleware;
using StarWarsTracker.Application.Implementation;
using StarWarsTracker.Persistence.Implementation;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Inject Dependencies
        builder.Services.InjectPersistenceDependencies(builder.Configuration.GetConnectionString("Default"));

        var env = builder.Environment.EnvironmentName;

        var loggingConfigs = builder.Configuration.GetSection($"Logging:Environment:{env}").Get<Dictionary<string, Dictionary<string, Dictionary<string, string>>>>();

        builder.Services.InjectApplicationDependencies(loggingConfigs);

        // Inject Middleware
        builder.Services.AddTransient<ExceptionHandlingMiddleware>();
        builder.Services.AddTransient<LoggingMiddleware>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        // Use Middleware
        app.UseMiddleware<LoggingMiddleware>();
        app.UseMiddleware<ExceptionHandlingMiddleware>();

        app.Run();
    }
}

