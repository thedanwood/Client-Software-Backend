using HaulageSystem.Domain.Enums;

namespace HaulageSystem.Application.Helpers;

public static class RoutingHelpers
{
    public static Dictionary<string, string> FormatParameter(RoutingParameters parameter, string parameterValue, RoutingProviders provider)
    {
        string mappedName = MapParameterName[provider][parameter];
        return new()
        {
            { mappedName, parameterValue }
        };
    }

    private static readonly Dictionary<RoutingProviders, Dictionary<RoutingParameters, string>> MapParameterName = new Dictionary<RoutingProviders, Dictionary<RoutingParameters, string>>
    {
        { RoutingProviders.HereMaps, new ()
            {
                { RoutingParameters.Traffic, "traffic" },
            }
        },
    };
    
    public static int IncreaseNoTrafficJourneyTime(int hasTrafficJourneyTime)
    {
        var journeyTime = hasTrafficJourneyTime * 1.08;
        return (int)journeyTime;
    }
}