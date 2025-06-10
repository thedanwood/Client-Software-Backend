using HaulageSystem.Application.Domain.Dtos.Materials;
using HaulageSystem.Application.Domain.Entities.Materials;
using HaulageSystem.Application.Models.Requests;
using OneOf;
using OneOf.Types;

namespace HaulageSystem.Application.Domain.Interfaces.Repositories;

public interface IMaterialPricingRepository
{
    Task<List<GetDepotMaterialPriceResponse>> GetMaterialPricingsByDepotMaterialPriceIdsAsync(List<int> ids);
    Task<GetDepotMaterialPriceResponse> GetMaterialPricingByDepotMaterialPriceId(int id);
    Task<List<GetDepotMaterialPriceResponse>> GetMaterialPricingByMaterialIdAndUnit(int pricingId, int unitId, bool includeNonActives = false);
    Task<List<GetDepotMaterialPriceResponse>> GetMaterialPricingByDepotIdAsync(int id);

    Task<GetDepotMaterialPriceResponse> GetMaterialPricingByMaterialIdDepotIdAndUnitIdAsync(int depotId, int materialId,
        int unitId);
    Task<int> CreateMaterialPricingAsync(CreateMaterialPricingRequest createRequest);
    Task UpdateMaterialPricingAsync(UpdateMaterialPricingRequest createRequest);
    Task UpdateMaterialPricingActiveStateAsync(int DepotMaterialPriceId, int activeState);
    Task DeleteMaterialPricingsAsync(List<int> ids);
}