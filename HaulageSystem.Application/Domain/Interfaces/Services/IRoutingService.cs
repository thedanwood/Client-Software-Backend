using HaulageSystem.Application.Dtos.Quotes;
using HaulageSystem.Application.Models.Routing;
using HaulageSystem.Domain.Enums;

namespace HaulageSystem.Application.Domain.Interfaces.Services;

public interface IRoutingService
{
    Task<List<JouneyTimeHasTrafficDto>> GetJourneyTimesWithHasTrafficOptions(RoutePoint startLocation,
        RoutePoint endLocation);

    Task<JouneyTimeHasTrafficDto> GetJourneyTimeWithHasTrafficEnabled(RoutePoint startLocation,
        RoutePoint endLocation, bool hasTrafficEnabled);
    Dictionary<RoutingParameters, string> GetRoutingParameters(bool hasTrafficEnabled);
    string GetFormattedRoutingParametersString(Dictionary<RoutingParameters, string> parameters);
    Task<int> GetTotalJourneyTimeInMinutes(RoutePoint routePoint1, RoutePoint routePoint2, Dictionary<RoutingParameters,string>? parameters = null);
}