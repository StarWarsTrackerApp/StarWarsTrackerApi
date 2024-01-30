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
        builder.Services.InjectApplicationDependencies();

        // Inject Middleware
        builder.Services.AddTransient<ExceptionHandlingMiddleware>();

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
        app.UseMiddleware<ExceptionHandlingMiddleware>();

        app.Run();
    }
}

