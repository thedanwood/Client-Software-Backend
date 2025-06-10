using HaulageSystem.Application.Core.Commands.Materials;
using HaulageSystem.Application.Domain.Interfaces.Repositories;
using HaulageSystem.Application.Dtos.Materials;
using HaulageSystem.Application.Extensions;
using HaulageSystem.Domain.Enums;
using MediatR;

namespace HaulageSystem.Application.Commands.Materials;

public class GetMaterialsQueryHandler : IRequestHandler<GetMaterialsQuery, List<MaterialInformationDto>>
{
    private readonly IMaterialPricingRepository _materialPricingRepository;
    private readonly IMaterialsRepository _materialsRepository;
    public GetMaterialsQueryHandler(IMaterialPricingRepository materialPricingRepository , IMaterialsRepository materialsRepository)
    {
        _materialPricingRepository = materialPricingRepository ?? throw new ArgumentNullException(nameof(materialPricingRepository));
        _materialsRepository = materialsRepository ?? throw new ArgumentNullException(nameof(materialsRepository));
    }

    public async Task<List<MaterialInformationDto>> Handle(GetMaterialsQuery query, CancellationToken cancellationToken)
    {
        List<MaterialInformationDto> materialInfo = new(); 
        
        var materials = await _materialsRepository.GetMaterials();

        foreach (var x in materials)
        {
            var dto = new MaterialInformationDto()
            {
                MaterialName = x.MaterialName,
                MaterialId = x.MaterialId
            };
            
            var materialPricings = await _materialPricingRepository.GetMaterialPricingByMaterialIdAndUnit(x.MaterialId, (int)MaterialUnits.Tonnes);
            if (materialPricings.Any())
            {
                if(materialPricings.Count() == 0)
                {
                    dto.SinglePrice = materialPricings.First().MaterialPricePerQuantityUnit;
                }
                var orderedPricing = materialPricings.Select(x => x.MaterialPricePerQuantityUnit).OrderDescending().ToList();
                if(orderedPricing.First() == orderedPricing.Last())
                {
                    dto.SinglePrice = materialPricings.First().MaterialPricePerQuantityUnit;
                }
                dto.HighestPrice = orderedPricing.First();
                dto.LowestPrice = orderedPricing.Last();
            }
            
            materialInfo.Add(dto);
        }

        return materialInfo.OrderBy(x=>x.MaterialName).ToList();
    }
}