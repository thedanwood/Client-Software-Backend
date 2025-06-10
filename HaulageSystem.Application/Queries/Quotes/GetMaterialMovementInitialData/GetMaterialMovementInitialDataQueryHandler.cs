using System.Globalization;
using HaulageSystem.Application.Core.Commands.Materials;
using HaulageSystem.Application.Core.Commands.Quotes;
using HaulageSystem.Application.Domain.Dtos.Materials;
using HaulageSystem.Application.Domain.Dtos.Quotes;
using HaulageSystem.Application.Domain.Interfaces.Repositories;
using HaulageSystem.Application.Domain.Interfaces.Services;
using HaulageSystem.Application.Dtos.Materials;
using HaulageSystem.Application.Exceptions;
using HaulageSystem.Application.Extensions;
using HaulageSystem.Application.Helpers;
using HaulageSystem.Application.Models.Routing;
using HaulageSystem.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Serilog;
using ILogger = Serilog.ILogger;

namespace HaulageSystem.Application.Commands.Quotes;

public class
    GetMaterialMovementInitialDataQueryHandler : IRequestHandler<GetMaterialMovementInitialDataQuery,
    List<MaterialMovementForDisplayDto>>
{
    private readonly IMaterialPricingRepository _materialPricingRepository;
    private readonly IDepotsRepository _depotsRepository;
    private readonly IMaterialsRepository _materialsRepository;
    private readonly IDeliveryService _deliveryService;
    private readonly IRoutingService _routingService;
    private readonly IMediator _mediator;

    public GetMaterialMovementInitialDataQueryHandler(IMaterialPricingRepository materialPricingRepository,
        IMaterialsRepository materialsRepository, IDepotsRepository depotsRepository, IRoutingService routingService, 
        IMediator mediator, IDeliveryService deliveryService)
    {
        _materialPricingRepository = materialPricingRepository;
        _materialsRepository = materialsRepository;
         _depotsRepository = depotsRepository;
          _routingService = routingService;
          _mediator = mediator;
          _deliveryService = deliveryService;
    }

    public async Task<List<MaterialMovementForDisplayDto>> Handle(GetMaterialMovementInitialDataQuery query,
        CancellationToken cancellationToken)
    {
        var materialPricings =
            (await _materialPricingRepository.GetMaterialPricingByMaterialIdAndUnit(query.MaterialId,
                query.MaterialUnitId))
            .Where(x => x.MaterialPricePerQuantityUnit != 0);

        var formattedPricings = materialPricings
            .GroupBy(r => r.DepotId)
            .Select(g => g.OrderByDescending(r => r.DepotMaterialPriceId).First())
            .ToList();

        var depots =
            await _depotsRepository.GetDepot(materialPricings.Select(x => x.DepotId).ToList());

        var displayInfo = new List<MaterialMovementForDisplayDto>();
        foreach (var materialPricing in formattedPricings)
        {
            var depot = depots.FirstOrDefault(x => x.DepotId == materialPricing.DepotId);

            var journeyTime = 0;
            if (query.JourneyTime.HasValue)
            {
                journeyTime = query.JourneyTime.Value;
            }
            else
            {
                var startLocation = new RoutePoint(depot.Latitude, depot.Longitude);
                var deliveryLocation = new RoutePoint(query.DeliveryLocationLatitude, query.DeliveryLocationLongitude);
                
                if (query.HasTrafficEnabled)
                {
                    var hasNoTrafficJourneyTime = (await _routingService.GetJourneyTimeWithHasTrafficEnabled(startLocation, deliveryLocation, false)).JourneyTime;
                    journeyTime = RoutingHelpers.IncreaseNoTrafficJourneyTime(hasNoTrafficJourneyTime);
                }
                else
                {
                    journeyTime = (await _routingService.GetJourneyTimeWithHasTrafficEnabled(startLocation, deliveryLocation, query.HasTrafficEnabled)).JourneyTime;
                }
               
            }
            

            displayInfo.Add(new MaterialMovementForDisplayDto()
            {
                JourneyTime = journeyTime,
                DepotName = depot.DepotName,
                DepotMaterialPriceId = materialPricing.DepotMaterialPriceId,
                Price = materialPricing.MaterialPricePerQuantityUnit
            });
        }

        return displayInfo;
    }
}