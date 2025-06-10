using HaulageSystem.Application.Models.Depots;
using HaulageSystem.Application.Models.Requests;

namespace HaulageSystem.Application.Domain.Interfaces.Repositories;

public interface IDepotsRepository
{
    Task<int> CreateDepot(string depotName, decimal latitude, decimal longitude, string fullAddress, bool isActive);
    Task DeleteDepots(List<int> depotIds);
    Task UpdateDepot(UpdateDepotRequest request);
    Task<GetDepotResponse> GetDepot(int Id);
    Task<List<GetDepotResponse>> GetDepot(List<int> ids);
    Task<List<GetDepotResponse>> GetDepots(List<int> depotIds, bool includeInactive = false);
    Task<List<GetDepotResponse>> GetAllDepots(bool includeInactive = false);
}