using System.Globalization;
using HaulageSystem.Application.Core.Commands.Materials;
using HaulageSystem.Application.Core.Commands.Quotes;
using HaulageSystem.Application.Domain.Dtos.Materials;
using HaulageSystem.Application.Domain.Dtos.Quotes;
using HaulageSystem.Application.Domain.Interfaces.Repositories;
using HaulageSystem.Application.Domain.Interfaces.Services;
using HaulageSystem.Application.Dtos.Materials;
using HaulageSystem.Application.Dtos.Quotes;
using HaulageSystem.Application.Exceptions;
using HaulageSystem.Application.Extensions;
using HaulageSystem.Application.Models.Routing;
using HaulageSystem.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Serilog;

namespace HaulageSystem.Application.Commands.Quotes;

public class GetNewDeliveryOnlyMovementPricingQueryHandler : IRequestHandler<GetNewDeliveryOnlyMovementPricingQuery,
    AdjustDeliveryQuotePricingDto>
{
    private readonly IBillingService _billingService;
    private readonly IRoutingService _routingService;
    private readonly IVehiclesRepository _vehiclesRepository;


    public GetNewDeliveryOnlyMovementPricingQueryHandler(IRoutingService routingService, IBillingService billingService, IVehiclesRepository vehiclesRepository)
    {
          _routingService = routingService;
          _billingService = billingService;
          _vehiclesRepository = vehiclesRepository;
    }

    public async Task<AdjustDeliveryQuotePricingDto> Handle(GetNewDeliveryOnlyMovementPricingQuery query,
        CancellationToken cancellationToken)
    {
        var deliveryPricing = await _billingService.CalculateDeliveryMovementQuotePriceAsync(
            new HaulageQuotePriceRequestDto()
            {
                StartLocation = query.StartLocation,
                DeliveryLocation = query.DeliveryLocation,
                OnewayJourneyTime = query.OnewayJourneyTime,
                VehicleTypeId = query.VehicleTypeId,
                NumberOfLoads = query.NumberOfLoads
            });
        
        return new AdjustDeliveryQuotePricingDto()
        {
            DefaultOnewayJourneyTime = query.OnewayJourneyTime,
            TotalDeliveryPrice = deliveryPricing.TotalDeliveryPrice,
            DefaultTotalDeliveryPrice = deliveryPricing.TotalDeliveryPrice,
            DeliveryPricePerTimeUnit = deliveryPricing.DeliveryPricePerTimeUnit,
            DefaultDeliveryPricePerTimeUnit = deliveryPricing.DeliveryPricePerTimeUnit,
            //todo from user settings?
            DeliveryUnitName = "Minute",
            VehicleTypeName = (await _vehiclesRepository.GetVehicleType(query.VehicleTypeId)).Name,
            NumberOfLoads = query.NumberOfLoads,
        };
    }
}