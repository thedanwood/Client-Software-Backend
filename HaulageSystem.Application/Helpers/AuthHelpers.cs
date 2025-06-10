using HaulageSystem.Domain.Enums;

namespace HaulageSystem.Application.Helpers;

public static class AuthHelpers
{
    public static List<UserRoles> FormatRoles(List<string> roles)
    {
        return roles.Select(x =>
        {
            switch (x)
            {
                case "admin":
                    return UserRoles.Admin;
                default:
                    throw new DataMisalignedException($"No role enum matches role string {x}");
            }
        }).ToList();
    }

    private static readonly Dictionary<RoutingProviders, Dictionary<RoutingParameters, string>> MapParameterName = new Dictionary<RoutingProviders, Dictionary<RoutingParameters, string>>
    {
        { RoutingProviders.HereMaps, new ()
            {
                { RoutingParameters.Traffic, "traffic" },
            }
        },
    };
}