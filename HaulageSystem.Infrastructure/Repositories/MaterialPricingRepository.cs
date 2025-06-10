using HaulageSystem.Application.Domain.Entities.Materials;
using HaulageSystem.Application.Domain.Interfaces.Repositories;
using HaulageSystem.Application.Domain.Interfaces.Services;
using HaulageSystem.Application.Domain.Mappers;
using HaulageSystem.Application.Dtos.Quotes;
using HaulageSystem.Application.Exceptions;
using HaulageSystem.Application.Helpers;
using HaulageSystem.Application.Models.Requests;
using HaulageSystem.Domain.Enums;
using HaulageSystem.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using OneOf;
using OneOf.Types;

namespace HaulageSystem.Shared.Repositories;

public class MaterialPricingRepository : IMaterialPricingRepository
{
    private readonly HaulagePlannerDbContext _context;
    private readonly IUserService _userService;

    public MaterialPricingRepository(HaulagePlannerDbContext context, IUserService userService)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _userService = userService;
    }

    public async Task<GetDepotMaterialPriceResponse> GetMaterialPricingByDepotMaterialPriceId(int id)
    {
        var pricing = await _context.DepotMaterialPrices.FirstOrDefaultAsync(x => x.DepotMaterialPriceId == id);
        return pricing.ToResponse();
    }

    public async Task<List<GetDepotMaterialPriceResponse>>
        GetMaterialPricingsByDepotMaterialPriceIdsAsync(List<int> ids)
    {
        var pricing = await _context.DepotMaterialPrices.Where(x => ids.Contains(x.DepotMaterialPriceId))
            .ToListAsync();
        return pricing.Select(x => x.ToResponse()).ToList();
    }

    public async Task<GetDepotMaterialPriceResponse>
        GetMaterialPricingByMaterialIdDepotIdAndUnitIdAsync(int depotId, int materialId, int unitId)
    {
        var pricing = await _context.DepotMaterialPrices
            .Where(x => x.MaterialId == materialId && x.DepotId == depotId && x.MaterialUnitEnum == unitId)
            .FirstOrDefaultAsync();

        if (pricing == null) return null;

        return pricing.ToResponse();
    }

    public async Task<List<GetDepotMaterialPriceResponse>> GetMaterialPricingByMaterialIdAndUnit(int materialId,
        int unitId, bool includeNonActives = false)
    {
        var pricing = await _context.DepotMaterialPrices
            .Where(x => x.MaterialId == materialId && x.MaterialUnitEnum == unitId).ToListAsync();
        if (!includeNonActives)
        {
            pricing = pricing.Where(x => x.ActiveState == DepotMaterialPricesActiveStates.Active.ToInt()).ToList();
        }
        return pricing.Select(x => x.ToResponse()).ToList();
    }

    public async Task<List<GetDepotMaterialPriceResponse>> GetMaterialPricingByDepotIdAsync(int id)
    {
        var pricing = await _context.DepotMaterialPrices
            .Where(x => x.DepotId == id && x.ActiveState == DepotMaterialPricesActiveStates.Active.ToInt())
            .ToListAsync();
        return pricing.Select(x => x.ToResponse()).ToList();
    }

    public async Task<int> CreateMaterialPricingAsync(CreateMaterialPricingRequest createRequest)
    {
        var pricing = createRequest.ToEntity();
        _context.DepotMaterialPrices.Add(pricing);
        await _context.SaveChangesAsync(await _userService.GetCurrentUsername());
        return pricing.DepotMaterialPriceId;
    }

    public async Task UpdateMaterialPricingAsync(UpdateMaterialPricingRequest updateRequest)
    {
        var pricing =
            await _context.DepotMaterialPrices.FirstOrDefaultAsync(x =>
                x.DepotMaterialPriceId == updateRequest.DepotMaterialPriceId);

        _context.Entry(pricing).CurrentValues.SetValues(updateRequest.ToEntity());
        await _context.SaveChangesAsync(await _userService.GetCurrentUsername());
    }
    
    public async Task UpdateMaterialPricingActiveStateAsync(int DepotMaterialPriceId, int activeState)
    {
        var pricing = _context.DepotMaterialPrices.FirstOrDefault(x => x.DepotMaterialPriceId == DepotMaterialPriceId);

        if (pricing == null)
        {
            throw new MaterialPricingNotFoundException(DepotMaterialPriceId);
        }
        
        pricing.ActiveState = activeState;
        await _context.SaveChangesAsync(await _userService.GetCurrentUsername());
    }

    public async Task DeleteMaterialPricingsAsync(List<int> ids)
    {
        var pricings =
            await _context.DepotMaterialPrices.Where(x => ids.Contains(x.DepotMaterialPriceId)).ToListAsync();

        foreach (var pricing in pricings)
        {
            pricing.ActiveState = DepotMaterialPricesActiveStates.Archived.ToInt();
        }

        await _context.SaveChangesAsync(await _userService.GetCurrentUsername());
    }
}