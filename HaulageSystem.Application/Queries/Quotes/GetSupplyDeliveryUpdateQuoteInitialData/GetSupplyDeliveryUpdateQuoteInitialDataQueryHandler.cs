using System.Text.Json.Serialization;
using HaulageSystem.Application.Core.Commands.Quotes;
using HaulageSystem.Application.Core.Domain.Calculator;
using HaulageSystem.Application.Domain.Dtos.Materials;
using HaulageSystem.Application.Domain.Dtos.Quotes;
using HaulageSystem.Application.Domain.Dtos.Vehicles;
using HaulageSystem.Application.Domain.Interfaces.Repositories;
using HaulageSystem.Application.Domain.Interfaces.Services;
using HaulageSystem.Application.Dtos.Materials;
using HaulageSystem.Application.Dtos.Quotes;
using HaulageSystem.Application.Helpers;
using HaulageSystem.Application.Models.Quotes;
using HaulageSystem.Domain.Enums;
using MediatR;

namespace HaulageSystem.Application.Queries.Quotes;

public class GetSupplyDeliveryUpdateQuoteInitialDataQueryHandler : IRequestHandler<GetSupplyDeliveryUpdateQuoteInitialDataQuery, UpdateQuoteSupplyDeliveryInitialDataDto>
{
    private readonly IDeliveryService _deliveryService;
    private readonly IQuotesRepository _quotesRepository;
    private readonly IQuotesService _quotesService;
    private readonly IVehiclesRepository _vehiclesRepository;
    private readonly ICompaniesRepository _companiesRepository;
    private readonly IMaterialPricingRepository _materialPricingRepository;
    private readonly IMaterialsRepository _materialsRepository;
    private readonly IMaterialsService _materialsService;
    private readonly IDepotsRepository _depotsRepository;
    private readonly IProfileService _profileService;
    private readonly IMediator _mediator;
    
    public GetSupplyDeliveryUpdateQuoteInitialDataQueryHandler(IQuotesRepository quotesRepository, IDeliveryService deliveryService, ICompaniesRepository companiesRepository, IMaterialPricingRepository materialPricingRepository, IMaterialsRepository materialsRepository, IMaterialsService materialsService, IMediator mediator, IDepotsRepository depotsRepository, IVehiclesRepository vehiclesRepository, IQuotesService quotesService, IProfileService profileService)
    {
        _quotesRepository = quotesRepository;
        _deliveryService = deliveryService;
        _companiesRepository = companiesRepository;
        _materialPricingRepository = materialPricingRepository;
        _materialsRepository = materialsRepository;
        _materialsService = materialsService;
        _mediator = mediator;
        _depotsRepository = depotsRepository;
        _vehiclesRepository = vehiclesRepository;
        _quotesService = quotesService;
        _profileService = profileService;
    }
    public async Task<UpdateQuoteSupplyDeliveryInitialDataDto> Handle(GetSupplyDeliveryUpdateQuoteInitialDataQuery query, CancellationToken cancellationToken)
    {
        var quote = await _quotesRepository.GetQuote(query.QuoteId);
        var deliveryMovements = await _quotesRepository.GetDeliveryMovementsAsync(query.QuoteId);
        var companyInfo = await _companiesRepository.GetCompanyByIdAsync(quote.CompanyID);
        var quotePricing = await _quotesService.GetSavedSupplyDeliveryQuotePricing(quote, deliveryMovements);
        
        var initialData = await _mediator.Send(new GetSupplyDeliveryQuoteInitialDataQuery());
        
        var updateDto = new UpdateQuoteSupplyDeliveryInitialDataDto()
        {
            VehicleTypes = initialData.VehicleTypes,
            Materials = initialData.Materials,
            MaterialUnits = initialData.MaterialUnits,
            DeliveryDate = quote.DeliveryDate,
            Comments = quote.Comments,
            CompanyInfo = new()
            {
                Name = companyInfo.CompanyName,
                Id = companyInfo.CompanyId
            },
            CustomerName = quote.CustomerName,
            NumberOfLoads = deliveryMovements[0].NumberOfLoads,
            DeliveryLocation = new AddressDto(new(quote.DeliveryLocationLatitude, quote.DeliveryLocationLongitude), quote.DeliveryLocationFullAddress),
            DefaultHasTrafficEnabled = _profileService.GetDefaultHasTrafficEnabled()
        };
        
        var updateMovements = new List<UpdateQuoteSupplyDeliveryMovementDto>();
        foreach (var deliveryMovement in deliveryMovements)
        {
            var movementPricing = quotePricing.FirstOrDefault(x => x.DeliveryMovementId == deliveryMovement.DeliveryMovementId);
            var materialMovement = await _quotesRepository.GetMaterialMovementAsync(deliveryMovement.DeliveryMovementId);
            var materialPricing =
                await _materialPricingRepository.GetMaterialPricingByDepotMaterialPriceId(materialMovement
                    .DepotMaterialPriceId);
            var depot = await _depotsRepository.GetDepot(materialPricing.DepotId);
            var material = await _materialsRepository.GetMaterial(materialPricing.MaterialId);
            var materialUnit = await _materialsService.GetMaterialUnit(materialPricing.MaterialUnitId);
            
            updateMovements.Add(new UpdateQuoteSupplyDeliveryMovementDto()
            {
                MaterialMovementId = materialMovement.MaterialMovementId,
                NumberOfLoads = movementPricing.MaterialPricing.NumberOfLoads,
                DepotName = depot.DepotName,
                MaterialId = material.MaterialId,
                MaterialUnitId = materialUnit.UnitId,
                Quantity = materialMovement.Quantity,
                VehicleTypeId = deliveryMovement.VehicleTypeId,
                DepotMaterialPriceId = materialMovement.DepotMaterialPriceId,
                DefaultOnewayJourneyTime = movementPricing.DefaultOnewayJourneyTime,
                OnewayJourneyTime = movementPricing.OnewayJourneyTime,
                DefaultTotalDeliveryPrice = movementPricing.DeliveryPricing.DefaultTotalDeliveryPrice,
                TotalDeliveryPrice = movementPricing.DeliveryPricing.TotalDeliveryPrice,
                DefaultDeliveryPricePerMinute = movementPricing.DeliveryPricing.DefaultDeliveryPricePerTimeUnit,
                DeliveryPricePerMinute = movementPricing.DeliveryPricing.DeliveryPricePerTimeUnit,
                TotalMaterialPrice = movementPricing.MaterialPricing.TotalMaterialPrice,
                DefaultTotalMaterialPrice = movementPricing.MaterialPricing.DefaultTotalMaterialPrice,
                MaterialPricePerQuantityUnit = movementPricing.MaterialPricing.MaterialPricePerQuantityUnit,
                DefaultMaterialPricePerQuantityUnit = movementPricing.MaterialPricing.MaterialPricePerQuantityUnit,
                MaterialAndDeliveryPricePerQuantityUnit = movementPricing.MaterialAndDeliveryPricePerQuantityUnit,
                DefaultMaterialAndDeliveryPricePerQuantityUnit = movementPricing.DefaultMaterialAndDeliveryPricePerQuantityUnit,
                HasTrafficEnabled = deliveryMovement.HasTrafficEnabled,
                DepotPricings = await GetDepotPricings(deliveryMovement.OnewayJourneyTime, materialMovement.DepotMaterialPriceId, deliveryMovement.HasTrafficEnabled, material.MaterialId, materialUnit.UnitId, quote.DeliveryLocationLatitude, quote.DeliveryLocationLongitude)
            });
        }

        updateDto.DeliveryMovements = updateMovements;
        return updateDto;
    }

    private async Task<List<UpdateQuoteSupplyDeliveryDepotPricing>> GetDepotPricings(int journeyTime, int selectedDepotMaterialPriceId, bool hasTrafficEnabled, int materialId, int unitId, decimal deliveryLocationLatitude, decimal deliveryLocationLongitude)
    {
        var depotPricings = await _mediator.Send(new GetMaterialMovementInitialDataQuery()
        {
            HasTrafficEnabled = hasTrafficEnabled,
            MaterialId = materialId,
            MaterialUnitId = unitId,
            DeliveryLocationLatitude = deliveryLocationLatitude,
            DeliveryLocationLongitude = deliveryLocationLongitude,
        });

        var originalPricing = depotPricings.FindIndex(x => x.DepotMaterialPriceId == selectedDepotMaterialPriceId);
        depotPricings[originalPricing].JourneyTime = journeyTime;

        var depotPricingsContainsSelected = depotPricings.Any(x => x.DepotMaterialPriceId == selectedDepotMaterialPriceId);
        if (!depotPricingsContainsSelected)
        {
            var materialPricing =
               await _materialPricingRepository.GetMaterialPricingByDepotMaterialPriceId(selectedDepotMaterialPriceId);
            var depot = await _depotsRepository.GetDepot(materialPricing.DepotId);

            depotPricings.Add(new MaterialMovementForDisplayDto()
            {
                JourneyTime = journeyTime,
                DepotName = depot.DepotName,
                DepotMaterialPriceId = materialPricing.DepotMaterialPriceId,
                Price = materialPricing.MaterialPricePerQuantityUnit
            });
        }

        return depotPricings.Select(x => new UpdateQuoteSupplyDeliveryDepotPricing()
        {
            DepotMaterialPriceId = x.DepotMaterialPriceId,
            DepotName = x.DepotName,
            Price = x.Price,
            JourneyTime = x.JourneyTime
        }).ToList();
    }
}