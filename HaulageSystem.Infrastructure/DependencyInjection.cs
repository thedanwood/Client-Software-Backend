using HaulageSystem.Application.ApiClients;
using HaulageSystem.Application.Configuration;
using HaulageSystem.Application.Configuration.ApiOptions;
using HaulageSystem.Application.Core.Domain.Interfaces.ApiClients;
using HaulageSystem.Application.Domain.Interfaces.Repositories;
using HaulageSystem.Application.Domain.Interfaces.Services;
using HaulageSystem.Application.Factories;
using HaulageSystem.Application.Helpers;
using HaulageSystem.Application.Models.Identity;
using HaulageSystem.Domain.Interfaces;
using HaulageSystem.Peristance.Interfaces;
using HaulageSystem.Persistance.Contexts;
using HaulageSystem.Shared.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HaulageSystem.Infrastructure.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment hostEnvironment)
    {
            services.AddDbContext<HaulagePlannerDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString(nameof(ConnectionStrings.HaulagePlannerDatabase)));
                LoggingHelper.ConfigureEntityFrameworkLogging(options, hostEnvironment);
            });
        
        services.AddTransient<IHaulagePlannerDbContext, HaulagePlannerDbContext>();

        return services;
    }

    public static IServiceCollection AddIdentityConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<SecurityStampValidatorOptions>(options =>
        {
            options.ValidationInterval = TimeSpan.FromDays(AppSettings.CookieExpiryInDays);
        });
        
        services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 5;
                options.Password.RequireNonAlphanumeric = false;
            })
            .AddEntityFrameworkStores<HaulagePlannerDbContext>();

        services.ConfigureApplicationCookie(options =>
        {
            options.Cookie.Name = AppSettings.CookieName;
            options.ExpireTimeSpan = TimeSpan.FromDays(AppSettings.CookieExpiryInDays);
            options.LoginPath = "/api/v1/auth/login";
            options.LogoutPath = "/api/v1/auth/logout";
            options.SlidingExpiration = true;
        });
        
        return services;
    }

    public static IServiceCollection AddConfigOptions(this IServiceCollection services,
        IConfiguration configuration)
    {
        var hereMapsRoutingApiClientOptions = configuration.GetSection(nameof(HereMapsRoutingApiClientOptions)).Get<HereMapsRoutingApiClientOptions>();
        services.Configure<HereMapsRoutingApiClientOptions>(x =>
        {
            x.ApiKey = hereMapsRoutingApiClientOptions.ApiKey;
            x.BaseUrl = hereMapsRoutingApiClientOptions.BaseUrl;
        });
        var sendGridOptions = configuration.GetSection(nameof(SendGridOptions)).Get<SendGridOptions>();
        services.Configure<SendGridOptions>(x =>
        {
            x.ApiKey = sendGridOptions.ApiKey;
        });
        return services;
    }

    public static IServiceCollection AddSharedInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddTransient<IMailService, MailService>();
        services.AddTransient<ICompaniesRepository, CompaniesRepository>();
        services.AddTransient<IQuotesRepository, QuotesRepository>();
        services.AddTransient<IMaterialsRepository, MaterialsRepository>();
        services.AddTransient<IVehiclesRepository, VehiclesRepository>();
        services.AddTransient<IDepotsRepository, DepotsRepository>();
        services.AddTransient<IDeliveryRepository, DeliveryRepository>();
        services.AddTransient<IMaterialPricingRepository, MaterialPricingRepository>();

        services.AddScoped<IHereMapsApiService, HereMapsApiService>();
        
        //TODO right lifetime?
        services.AddScoped<IApiClientFactory, ApiClientFactory>();

        services.AddHttpClient(ApiClientNames.HereMaps, client =>
        {
            var options = configuration.GetSection(nameof(HereMapsRoutingApiClientOptions))
                .Get<HereMapsRoutingApiClientOptions>();
            client.BaseAddress = new Uri(options.BaseUrl);
        });

        return services;
    }
}