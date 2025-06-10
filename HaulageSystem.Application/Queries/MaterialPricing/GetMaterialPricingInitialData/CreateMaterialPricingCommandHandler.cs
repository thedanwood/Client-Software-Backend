using HaulageSystem.Application.Domain.Entities.Database;
using HaulageSystem.Application.Domain.Interfaces.Repositories;
using HaulageSystem.Application.Dtos.Materials;
using HaulageSystem.Application.Dtos.Quotes;
using HaulageSystem.Application.Mappers;
using MediatR;

namespace HaulageSystem.Application.Commands.MaterialPricing;

public class GetMaterialPricingInitialDataCommandHandler : IRequestHandler<GetMaterialPricingInitialDataCommand, MaterialPricingInitialDataDto>
{
    private readonly IMaterialsRepository _materialsRepository;
    private readonly IDepotsRepository _depotsRepository;

    public GetMaterialPricingInitialDataCommandHandler(IMaterialsRepository materialRepository, IDepotsRepository depotsRepository)
    {
        _materialsRepository = materialRepository ??  throw new ArgumentNullException(nameof(materialRepository));
         _depotsRepository = depotsRepository ??  throw new ArgumentNullException(nameof(depotsRepository));
    }

    public async Task<MaterialPricingInitialDataDto> Handle(GetMaterialPricingInitialDataCommand command, CancellationToken cancellationToken)
    {
        var materials = (await _materialsRepository.GetMaterials()).Select(x => new MaterialDto(x.MaterialId, x.MaterialName)).ToList();

        var depot = await _depotsRepository.GetDepot(command.DepotId);

        return new MaterialPricingInitialDataDto()
        {
            Materials = materials,
            DepotName = depot.DepotName,
        };
    }
}