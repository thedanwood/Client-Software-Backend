using Azure.Identity;
using HaulageSystem.Application.Domain.Interfaces.Services;
using HaulageSystem.Application.Domain.Services;
using HaulageSystem.Application.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HaulageSystem.Application;

public static class DependencyInjection
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(DependencyInjection));

        services.AddSingleton<IProfileService, ProfileService>();
        services.AddScoped<IBillingService, BillingService>();
        services.AddScoped<IDeliveryService, DeliveryService>();
        services.AddScoped<IMaterialsService, MaterialsService>();
        services.AddScoped<IQuotesService, QuotesService>();
        services.AddScoped<IRoutingService, RoutingService>();
        services.AddScoped<IPdfService, PdfService>();
        services.AddScoped<IUserService, UserService>();
    }

    public static WebApplicationBuilder AddKeyVault(this WebApplicationBuilder builder)
    {
        if (builder.Environment.IsProduction())
        {
            string environment = Environment.GetEnvironmentVariable("ENVIRONMENT");
            string jsonFile = $"appsettings.{environment}.json";

            builder.Configuration
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile(jsonFile, optional: true);

            string? keyVaultUrl = builder.Configuration["KeyVault"];

            var credentials = new ManagedIdentityCredential();

            builder.Configuration.AddAzureKeyVault(new Uri(keyVaultUrl), credentials);
        }

        return builder;
    }

    public static void SetupMiddleware(this IApplicationBuilder? app)
    {
        app.UseMiddleware<ErrorHandlerMiddleware>();
    }
}