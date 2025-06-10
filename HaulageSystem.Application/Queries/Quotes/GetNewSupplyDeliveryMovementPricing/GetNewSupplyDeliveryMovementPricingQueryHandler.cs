using System.Globalization;
using System.Security.Cryptography;
using HaulageSystem.Application.Core.Commands.Materials;
using HaulageSystem.Application.Core.Commands.Quotes;
using HaulageSystem.Application.Core.Domain.Calculator;
using HaulageSystem.Application.Domain.Dtos.Materials;
using HaulageSystem.Application.Domain.Dtos.Quotes;
using HaulageSystem.Application.Domain.Interfaces.Repositories;
using HaulageSystem.Application.Domain.Interfaces.Services;
using HaulageSystem.Application.Dtos.Materials;
using HaulageSystem.Application.Dtos.Quotes;
using HaulageSystem.Application.Exceptions;
using HaulageSystem.Application.Extensions;
using HaulageSystem.Application.Helpers;
using HaulageSystem.Application.Models.Routing;
using HaulageSystem.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Serilog;

namespace HaulageSystem.Application.Commands.Quotes;

public class GetNewSupplyDeliveryMovementPricingQueryHandler : IRequestHandler<GetNewSupplyDeliveryMovementPricingQuery,
    SupplyDeliveryMovementPricingDto>
{
    private readonly IBillingService _billingService;
    private readonly IDeliveryService _deliveryService;
    private readonly IVehiclesRepository _vehiclesRepository;
    private readonly IDepotsRepository _depotsRepository;
    private readonly IProfileService _profileService;
    private readonly IMaterialPricingRepository _materialPricingRepository;
    private readonly IMaterialsService _materialsService;


    public GetNewSupplyDeliveryMovementPricingQueryHandler(IBillingService billingService, IDepotsRepository depotsRepository, IMaterialPricingRepository materialPricingDepository, IDeliveryService deliveryService, IVehiclesRepository vehiclesRepository, IProfileService profileService, IMaterialsService materialsService)
    {
          _billingService = billingService;
          _deliveryService = deliveryService;
          _vehiclesRepository = vehiclesRepository;
          _profileService = profileService;
          _materialsService = materialsService;
          _materialPricingRepository = materialPricingDepository;
          _depotsRepository = depotsRepository;
    }

    public async Task<SupplyDeliveryMovementPricingDto> Handle(GetNewSupplyDeliveryMovementPricingQuery query,
        CancellationToken cancellationToken)
    {
        var materialUnit = _materialsService.GetMaterialUnitEnum(query.MaterialUnitId);
        
        var numberOfLoads = await _deliveryService.GetNumberOfLoads(materialUnit, query.Quantity, query.VehicleTypeId);
        var materialDepotPricing =
            await _materialPricingRepository.GetMaterialPricingByDepotMaterialPriceId(query.DepotMaterialPriceId);
        var depot = await _depotsRepository.GetDepot(materialDepotPricing.DepotId);
        var startLocation = new RoutePoint(depot.Latitude, depot.Longitude);
        
        var deliveryPricing = await _billingService.CalculateDeliveryMovementQuotePriceAsync(
            new HaulageQuotePriceRequestDto()
            {
                StartLocation = startLocation,
                DeliveryLocation = query.DeliveryLocation,
                OnewayJourneyTime = query.OnewayJourneyTime,
                VehicleTypeId = query.VehicleTypeId,
                NumberOfLoads = query.NumberOfLoads
            });
        
        var totalQuantity = numberOfLoads * query.Quantity;
        var materialPricing =
            await _billingService.CalculateMaterialMovementQuotePrice(query.DepotMaterialPriceId, totalQuantity);

        var defaultTotalMaterialAndDeliveryPrice =
            QuoteCalculator.CalculateTotalMaterialAndDeliveryPrice(materialPricing.TotalMaterialPrice,
                deliveryPricing.TotalDeliveryPrice);
        var defaultMaterialAndDeliveryPricePerQuantityUnit = QuoteCalculator.CalculateMaterialPricePerQuantityUnit(defaultTotalMaterialAndDeliveryPrice, totalQuantity);

        return new()
        {
            DefaultOnewayJourneyTime = query.OnewayJourneyTime,
            DefaultTotalDeliveryPrice = deliveryPricing.TotalDeliveryPrice,
            TotalDeliveryPrice = deliveryPricing.TotalDeliveryPrice,
            DefaultDeliveryPricePerTimeUnit = deliveryPricing.DeliveryPricePerTimeUnit,
            DeliveryPricePerTimeUnit = deliveryPricing.DeliveryPricePerTimeUnit,
            DefaultTotalMaterialPrice = materialPricing.DefaultTotalMaterialPrice,
            TotalMaterialPrice = materialPricing.TotalMaterialPrice,
            DefaultMaterialPricePerQuantityUnit = materialPricing.MaterialPricePerQuantityUnit,
            MaterialPricePerQuantityUnit = materialPricing.MaterialPricePerQuantityUnit,
            DefaultMaterialAndDeliveryPricePerQuantityUnit = defaultMaterialAndDeliveryPricePerQuantityUnit,
            MaterialAndDeliveryPricePerQuantityUnit = defaultMaterialAndDeliveryPricePerQuantityUnit,
        };
    }
}