using HaulageSystem.Application.Core.Domain.Interfaces.ApiClients;
using HaulageSystem.Application.Configuration.ApiOptions;
using HaulageSystem.Application.Domain.Services;
using HaulageSystem.Application.Factories;
using HaulageSystem.Application.Configuration;
using HaulageSystem.Application.Domain.Dtos.ApiServices;
using HaulageSystem.Application.Helpers;
using HaulageSystem.Domain.Enums;
using HaulageSystem.Infrastructure.DependencyInjection.Models;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;
using ILogger = Serilog.ILogger;

namespace HaulageSystem.Application.ApiClients;

public class HereMapsApiService : IHereMapsApiService
{
    private readonly ExtendedHttpClient _apiClient;
    private readonly HereMapsRoutingApiClientOptions _apiOptions;
    private readonly ILogger<HereMapsApiService> _logger;

    public HereMapsApiService(
        IOptions<HereMapsRoutingApiClientOptions> apiOptions, IApiClientFactory apiClientFactory, ILogger<HereMapsApiService> logger)
    {
        _logger = logger;
        _apiOptions = apiOptions?.Value ?? throw new ArgumentNullException(nameof(apiOptions));
        _apiClient = apiClientFactory.CreateApiClient(ApiClientNames.HereMaps);
    }

    public async Task<HereMapsRouteInformation> GetRouteInformation(decimal longitude1, decimal latitude1,
        decimal longitude2, decimal latitude2, string routeParameters)
    {
        var url =
            $"calculateroute.json?apiKey={_apiOptions.ApiKey}&mode=fastest;truck&{routeParameters}&LimitedWeight=18&routesummarytype=traveltime" +
            $"&waypoint0=geo!{longitude1},{latitude1}&waypoint1=geo!{longitude2},{latitude2}";
        
        var retryPolicy = Policy<HereMapsRoutingApiResponse>
            .Handle<Exception>(ex => ex.Message.Contains("429"))
            .WaitAndRetryAsync(
                retryCount: 3,
                sleepDurationProvider: _ => TimeSpan.FromSeconds(1),
                onRetry: (exception, timeSpan, retryCount, context) =>
                {
                    if (retryCount > 1)
                    {
                        _logger.LogWarning($"Retry {retryCount} after {timeSpan.TotalSeconds} seconds due to {exception.Exception.Message}");
                    }
                }
            );
        
        var result = await retryPolicy.ExecuteAsync(async () =>
        {
            var response = await _apiClient.GetAsync<HereMapsRoutingApiResponse>(url);
            return response;
        });
        
        var travelTimeInSeconds = result.response.route[0].summary.travelTime;
        
        
        return new HereMapsRouteInformation()
        {
            TravelTimeInMinutes = DateTimeHelpers.ConvertSecondsToMinutes(travelTimeInSeconds)
        };
    }
}


