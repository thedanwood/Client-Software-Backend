using HaulageSystem.Application.Domain.Dtos.Materials;
using HaulageSystem.Application.Domain.Dtos.Vehicles;
using HaulageSystem.Application.Domain.Interfaces.Repositories;
using HaulageSystem.Application.Domain.Interfaces.Services;
using HaulageSystem.Application.Dtos.Materials;
using HaulageSystem.Application.Dtos.Quotes;
using MediatR;

namespace HaulageSystem.Application.Queries.Quotes;

public class GetSupplyDeliveryQuoteInitialDataQueryHandler : IRequestHandler<GetSupplyDeliveryQuoteInitialDataQuery,
    QuoteSupplyDeliveryInitialDataDto>
{
    private readonly IQuotesService _quotesService;
    private readonly IMaterialsRepository _materialsRepository;
    private readonly IMaterialsService _materialsService;
    private readonly IVehiclesRepository _vehiclesRepository;
    private readonly IProfileService _profileService;
    private readonly IDeliveryService _deliveryService;
    private readonly IRoutingService _routingService;

    public GetSupplyDeliveryQuoteInitialDataQueryHandler(IMaterialsRepository materialsRepository,
        IMaterialsService materialsService, IVehiclesRepository vehiclesRepository, IDeliveryService deliveryService, IQuotesService quotesService, IRoutingService routingService, IProfileService profileService)
    {
        _materialsRepository = materialsRepository;
        _materialsService = materialsService;
        _vehiclesRepository = vehiclesRepository;
        _deliveryService = deliveryService;
        _quotesService = quotesService;
        _routingService = routingService;
        _profileService = profileService;
    }

    public async Task<QuoteSupplyDeliveryInitialDataDto> Handle(GetSupplyDeliveryQuoteInitialDataQuery query,
        CancellationToken cancellationToken)
    {
        var units = await _materialsService.GetMaterialUnits();
        var materials = await _materialsRepository.GetMaterials();
        var vehicles = await _vehiclesRepository.GetAllVehicleTypes();

        return new QuoteSupplyDeliveryInitialDataDto()
        {
            MaterialUnits = units.Select(x => new MaterialUnitDto(x.UnitId, x.UnitName)).ToList(),
            Materials = materials.Select(x => new MaterialDto(x.MaterialId, x.MaterialName)).ToList(),
            VehicleTypes = vehicles.Select(x => new VehicleTypeDto(x.Id, x.Name, x.Capacity)).ToList(),
            DefaultHasTrafficEnabled = _profileService.GetDefaultHasTrafficEnabled()
        };
    }
}