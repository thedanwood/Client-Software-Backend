using HaulageSystem.Application.Core.Commands.Materials;
using HaulageSystem.Application.Domain.Interfaces.Repositories;
using HaulageSystem.Application.Dtos.Depots;
using HaulageSystem.Application.Models.Quotes;
using HaulageSystem.Application.Models.Routing;
using MediatR;

namespace HaulageSystem.Application.Commands.Materials;

public class GetDepotsQueryHandler : IRequestHandler<GetDepotsQuery, List<DepotsInformationDto>>
{
    private readonly IDepotsRepository _depotsRepository;
    private readonly IMaterialPricingRepository _materialPricingRepository;

    public GetDepotsQueryHandler(IDepotsRepository depotsRepository, IMaterialPricingRepository materialPricingRepository)
    {
        _depotsRepository = depotsRepository;
        _materialPricingRepository = materialPricingRepository;
    }

    public async Task<List<DepotsInformationDto>> Handle(GetDepotsQuery query, CancellationToken cancellationToken)
    {
        List<DepotsInformationDto> depotsInfo = new(); 
        
        var depots = await _depotsRepository.GetAllDepots();

        foreach (var x in depots)
        {
            var materialPricing = await _materialPricingRepository.GetMaterialPricingByDepotIdAsync(x.DepotId);

            var dto = new DepotsInformationDto()
            {
                DepotId = x.DepotId,
                DepotName = x.DepotName,
                DepotAddress = new AddressDto(new RoutePoint(x.Latitude, x.Longitude), x.Address),
                NumberOfSuppliedMaterials = materialPricing.DistinctBy(x => x.MaterialId).Count()
            };
            
            depotsInfo.Add(dto);
        }

        return depotsInfo.OrderBy(x=>x.DepotName).ToList();;
    }
}