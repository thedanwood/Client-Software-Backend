using Azure.Monitor.OpenTelemetry.AspNetCore;
using HaulageSystem.Application.Configuration.ApiOptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HaulageSystem.Application.Helpers;

public static class LoggingHelper
{
    public static WebApplicationBuilder ConfigureLogging(this WebApplicationBuilder builder, ConfigurationManager configuration, IHostEnvironment hostEnvironment)
    {
        if (hostEnvironment.IsProduction())
        {
            var connection = configuration.GetConnectionString(nameof(ConnectionStrings.AzureMonitor));

            builder.Services.AddOpenTelemetry().UseAzureMonitor(options =>
            {
                options.ConnectionString = connection;
            });

            builder.Logging.SetMinimumLevel(LogLevel.Warning);
        }
        else
        {
            builder.Logging.SetMinimumLevel(LogLevel.Information);
            builder.Logging.AddConsole();
        }
        return builder;
    }
    public static void ConfigureEntityFrameworkLogging(DbContextOptionsBuilder options, IHostEnvironment hostEnvironment)
    {
        if (!hostEnvironment.IsProduction())
        {
            options.LogTo(Console.WriteLine, LogLevel.Information);
        }
    }
}