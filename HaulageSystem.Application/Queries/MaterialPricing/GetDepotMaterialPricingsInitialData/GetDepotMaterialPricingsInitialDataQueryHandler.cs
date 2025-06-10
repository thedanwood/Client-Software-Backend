using System.Globalization;
using HaulageSystem.Application.Core.Commands.Materials;
using HaulageSystem.Application.Domain.Dtos.Materials;
using HaulageSystem.Application.Domain.Interfaces.Repositories;
using HaulageSystem.Application.Domain.Interfaces.Services;
using HaulageSystem.Application.Domain.Services;
using HaulageSystem.Application.Dtos.Materials;
using HaulageSystem.Application.Exceptions;
using HaulageSystem.Application.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace HaulageSystem.Application.Commands.Materials;

public class GetDepotMaterialPricingsInitialDataQueryHandler : IRequestHandler<GetDepotMaterialPricingsInitialDataQuery,
    DepotMaterialPricingInitialDataDto>
{
    private readonly IMaterialPricingRepository _materialPricingRepository;
    private readonly IMaterialsRepository _materialsRepository;
    private readonly IMaterialsService _materialsService;

    public GetDepotMaterialPricingsInitialDataQueryHandler(IMaterialPricingRepository materialPricingRepository,
        IMaterialsRepository materialsRepository, IMaterialsService materialsService)
    {
        _materialPricingRepository = materialPricingRepository;
        _materialsRepository = materialsRepository;
         _materialsService = materialsService;
    }

    public async Task<DepotMaterialPricingInitialDataDto> Handle(GetDepotMaterialPricingsInitialDataQuery query,
        CancellationToken cancellationToken)
    {
        var allMaterialUnits = (await _materialsService.GetMaterialUnits()).Select(x=> new MaterialUnitDto(x.UnitId, x.UnitName)).ToList();
        
        var materialPricings = await _materialPricingRepository.GetMaterialPricingByDepotIdAsync(query.DepotId);
        var pricingsByMaterialId = materialPricings.GroupBy(x => x.MaterialId);
        var filteredPricingsByMaterialId = pricingsByMaterialId.Select(materialGroup => new
        {
            MaterialId = materialGroup.Key,
            Prices = materialGroup
                .GroupBy(x => x.MaterialUnitId)
                .Select(unitGroup => unitGroup
                    .OrderByDescending(p => p.DepotMaterialPriceId)
                    .First())
                .ToList()
        }).ToList();

        var pricingsInfo = new List<DepotMaterialPricingsDto>();
        foreach(var pricingByMaterialId in filteredPricingsByMaterialId)
        {
            var materialId = pricingByMaterialId.MaterialId;
            var material = await _materialsRepository.GetMaterial(materialId);
            var pricings = pricingByMaterialId.Prices;
            var pricingInfo = new DepotMaterialPricingsDto()
            {
                MaterialId = materialId,
                MaterialName = material.MaterialName,
            };
                       
            foreach(var pricing in pricings)
            {
                var materialName = await _materialsService.GetMaterialUnit(pricing.MaterialUnitId);
                pricingInfo.Pricings.Add(new()
                {
                    DepotMaterialPriceId = pricing.DepotMaterialPriceId,
                    UnitId = pricing.MaterialUnitId,
                    UnitName = materialName.UnitName,
                    Price = pricing.MaterialPricePerQuantityUnit
                });
            }
            
            pricingsInfo.Add(pricingInfo);
        }

        return new()
        {
            AllMaterialUnits = allMaterialUnits,
            DepotMaterials = pricingsInfo.OrderBy(x => x.MaterialName).ToList()
        };
    }
}