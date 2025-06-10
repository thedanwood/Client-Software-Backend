using HaulageSystem.Application.Domain.Dtos.ApiServices;
using HaulageSystem.Domain.Enums;

namespace HaulageSystem.Application.Core.Domain.Interfaces.ApiClients;

public interface IHereMapsApiService
{
    Task<HereMapsRouteInformation> GetRouteInformation(decimal latitude1, decimal longitude1, decimal latitude2,
        decimal longitude2, string routeParameters);
}