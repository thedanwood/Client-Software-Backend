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

public class GetSavedDeliveryOnlyMovementPricingsQueryHandler : IRequestHandler<GetSavedDeliveryOnlyMovementPricingsQuery,
    List<DeliveryOnlyMovementPricingDto>>
{
    private readonly IQuotesRepository _quotesRepository;
    
    public GetSavedDeliveryOnlyMovementPricingsQueryHandler(IQuotesRepository quotesRepository)
    {
        _quotesRepository = quotesRepository;
    }

    public async Task<List<DeliveryOnlyMovementPricingDto>> Handle(GetSavedDeliveryOnlyMovementPricingsQuery query,
        CancellationToken cancellationToken)
    {
        var deliveryMovements = await _quotesRepository.GetDeliveryMovementsAsync(query.QuoteId);
        
        var movements = new List<DeliveryOnlyMovementPricingDto>();
        foreach (var deliveryMovement in deliveryMovements)
        {
            movements.Add(new()
            {
                DeliveryMovementId = deliveryMovement.DeliveryMovementId,
                DefaultOnewayJourneyTime = deliveryMovement.DefaultOnewayJourneyTime,
                DefaultTotalDeliveryPrice = deliveryMovement.DefaultTotalDeliveryPrice,
                TotalDeliveryPrice = deliveryMovement.TotalDeliveryPrice,
                DefaultDeliveryPricePerTimeUnit = deliveryMovement.DefaultDeliveryPricePerTimeUnit,
                DeliveryPricePerTimeUnit = deliveryMovement.DeliveryPricePerTimeUnit,
            });
        }

        return movements;
    }
}