using HaulageSystem.Application.Core.Domain.Interfaces.ApiClients;
using HaulageSystem.Application.Domain.Interfaces.Services;
using HaulageSystem.Application.Dtos.Quotes;
using HaulageSystem.Application.Helpers;
using HaulageSystem.Application.Models.Routing;
using HaulageSystem.Domain.Enums;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HaulageSystem.Application.Domain.Services;

public class RoutingService : IRoutingService
{
    private readonly IHereMapsApiService _apiClient;

    public RoutingService(IHereMapsApiService apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<List<JouneyTimeHasTrafficDto>> GetJourneyTimesWithHasTrafficOptions(RoutePoint startLocation, RoutePoint endLocation)
    {
        var journeyTimes = new List<JouneyTimeHasTrafficDto>();

        var noTrafficJourneyTime = await GetJourneyTimeWithHasTrafficEnabled(startLocation, endLocation, false);
        journeyTimes.Add(noTrafficJourneyTime);

        var withTrafficJourneyTime = new JouneyTimeHasTrafficDto
        {
            HasTrafficEnabled = true,
            JourneyTime = RoutingHelpers.IncreaseNoTrafficJourneyTime(noTrafficJourneyTime.JourneyTime)
        };
        journeyTimes.Add(withTrafficJourneyTime);

        return journeyTimes;
    }

    public async Task<JouneyTimeHasTrafficDto> GetJourneyTimeWithHasTrafficEnabled(RoutePoint startLocation,
        RoutePoint endLocation, bool hasTrafficEnabled)
    {
        var parameters = this.GetRoutingParameters(hasTrafficEnabled);
        var journeyTime = await this.GetTotalJourneyTimeInMinutes(
            new RoutePoint(startLocation.Latitude, startLocation.Longitude),
            new RoutePoint(endLocation.Latitude, endLocation.Longitude),
            parameters);

        return new()
        {
            HasTrafficEnabled = hasTrafficEnabled,
            JourneyTime = journeyTime
        };
    }
    public Dictionary<RoutingParameters, string> GetRoutingParameters(bool hasTrafficEnabled)
    {
        var parameters = new Dictionary<RoutingParameters, string>();
                   
        parameters.Add(RoutingParameters.Traffic, hasTrafficEnabled && hasTrafficEnabled ? "enabled" : "disabled");

        return parameters;
    }

    public string GetFormattedRoutingParametersString(Dictionary<RoutingParameters, string> parameters) 
    {
        return string.Join("&", parameters.Select(param => $"{param.Key.ToString().ToLower()}={param.Value.ToLower()}"));
    }

    public async Task<int> GetTotalJourneyTimeInMinutes(RoutePoint routePoint1, RoutePoint routePoint2, Dictionary<RoutingParameters,string>? parameters = null)
    {
        // if (_env.IsDevelopment())
        // {
        //     var random = new Random();
        //     return random.Next(1, 201);
        // }

        var response = await _apiClient.GetRouteInformation(
            routePoint1.Latitude, routePoint1.Longitude,
            routePoint2.Latitude, routePoint2.Longitude, 
            GetFormattedRoutingParametersString(parameters));

        return response.TravelTimeInMinutes;
    }
}