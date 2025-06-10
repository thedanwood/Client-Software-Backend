using System.Globalization;
using System.Security.Cryptography;
using HaulageSystem.Application.Core.Commands.Materials;
using HaulageSystem.Application.Core.Commands.Quotes;
using HaulageSystem.Application.Core.Domain.Calculator;
using HaulageSystem.Application.Domain.Dtos.Materials;
using HaulageSystem.Application.Domain.Dtos.Quotes;
using HaulageSystem.Application.Domain.Interfaces.Repositories;
using HaulageSystem.Application.Domain.Interfaces.Services;
using HaulageSystem.Application.Domain.Services;
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

public class GetSavedSupplyDeliveryMovementPricingsQueryHandler : IRequestHandler<GetSavedSupplyDeliveryMovementPricingsQuery,
    List<SupplyDeliveryMovementPricingDto>>
{
    private readonly IQuotesService _quotesService;
    private readonly IQuotesRepository _quotesRepository;
    
    public GetSavedSupplyDeliveryMovementPricingsQueryHandler(IQuotesService quotesService, IQuotesRepository quotesRepository)
    {
        _quotesService = quotesService;
        _quotesRepository = quotesRepository;
    }

    public async Task<List<SupplyDeliveryMovementPricingDto>> Handle(GetSavedSupplyDeliveryMovementPricingsQuery query,
        CancellationToken cancellationToken)
    {
        var quote = await _quotesRepository.GetQuote(query.QuoteId);
        var deliveryMovements = await _quotesRepository.GetDeliveryMovementsAsync(query.QuoteId);
        var quotePricing = await _quotesService.GetSavedSupplyDeliveryQuotePricing(quote, deliveryMovements);
        
        var movements = new List<SupplyDeliveryMovementPricingDto>();
        foreach (var deliveryMovement in deliveryMovements)
        {
            var movementPricing =
                quotePricing.FirstOrDefault(x => x.DeliveryMovementId == deliveryMovement.DeliveryMovementId);
            movements.Add(new()
            {
                DeliveryMovementId = deliveryMovement.DeliveryMovementId,
                MaterialMovementId = movementPricing.MaterialPricing.MaterialMovementId,
                DefaultOnewayJourneyTime = movementPricing.DefaultOnewayJourneyTime,
                DefaultTotalDeliveryPrice = movementPricing.DeliveryPricing.DefaultTotalDeliveryPrice,
                TotalDeliveryPrice = movementPricing.DeliveryPricing.TotalDeliveryPrice,
                DefaultDeliveryPricePerTimeUnit = movementPricing.DeliveryPricing.DefaultDeliveryPricePerTimeUnit,
                DeliveryPricePerTimeUnit = movementPricing.DeliveryPricing.DeliveryPricePerTimeUnit,
                TotalMaterialPrice = movementPricing.MaterialPricing.TotalMaterialPrice,
                DefaultTotalMaterialPrice = movementPricing.MaterialPricing.DefaultTotalMaterialPrice,
                MaterialPricePerQuantityUnit = movementPricing.MaterialPricing.MaterialPricePerQuantityUnit,
                DefaultMaterialPricePerQuantityUnit = movementPricing.MaterialPricing.DefaultMaterialPricePerQuantityUnit,
                DefaultMaterialAndDeliveryPricePerQuantityUnit = movementPricing.DefaultMaterialAndDeliveryPricePerQuantityUnit,
                MaterialAndDeliveryPricePerQuantityUnit = movementPricing.MaterialAndDeliveryPricePerQuantityUnit
            });
        }

        return movements;
    }
}