using StarWarsTracker.Api.Examples.Events;
using StarWarsTracker.Api.Middleware;
using StarWarsTracker.Application.Implementation;
using StarWarsTracker.Logging;
using StarWarsTracker.Logging.AppSettingsConfig;
using StarWarsTracker.Persistence.Implementation;
using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
            {
                Version = "v1",
                Title = "Star Wars Tracker"                
            });

            c.ExampleFilters();
            c.EnableAnnotations();

            // add XML Comments for Application Assembly (Where Requests are)
            var filePath = Path.Combine(AppContext.BaseDirectory, typeof(IRequest).Assembly.GetName().Name + ".xml");
            if (File.Exists(filePath))
            {
                c.IncludeXmlComments(filePath);
            }

            // add XML Comments for API Assembly (Where Controllers are)
            filePath = Path.Combine(AppContext.BaseDirectory, typeof(Program).Assembly.GetName().Name + ".xml");
            if (File.Exists(filePath))
            {
                c.IncludeXmlComments(filePath);
            }
        });
            
        builder.Services.AddSwaggerExamplesFromAssemblyOf<Program>();

        // Inject Dependencies
        builder.Services.InjectPersistenceDependencies(builder.Configuration.GetConnectionString("Default"));
        builder.Services.InjectApplicationDependencies();

        var env = builder.Environment.EnvironmentName;

        var loggingConfigs = builder.Configuration.GetSection($"Logging:Environment:{env}").Get<Dictionary<string, LogConfigSettings>>();

        builder.Services.InjectLoggingDependencies(loggingConfigs);

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
