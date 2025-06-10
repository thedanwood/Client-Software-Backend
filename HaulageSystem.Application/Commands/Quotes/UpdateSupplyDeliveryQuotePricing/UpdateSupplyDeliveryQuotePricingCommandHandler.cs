using System.Data;
using HaulageSystem.Application.Core.Commands.Quotes;
using HaulageSystem.Application.Domain.Entities.Quotes;
using HaulageSystem.Application.Domain.Interfaces.Repositories;
using HaulageSystem.Application.Domain.Interfaces.Services;
using HaulageSystem.Application.Dtos.Quotes;
using HaulageSystem.Application.Models.Quotes;
using HaulageSystem.Application.Models.Requests;
using HaulageSystem.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore.Query;

namespace HaulageSystem.Application.Commands.Quotes;

public class UpdateSupplyDeliveryQuotePricingCommandHandler : IRequestHandler<UpdateSupplyDeliveryQuotePricingCommand>
{
    private readonly IQuotesRepository _quotesRepository;
    private readonly IQuotesService _quotesService;
    private readonly IUserService _userService;

    public UpdateSupplyDeliveryQuotePricingCommandHandler(IQuotesRepository quotesRepository,
        IUserService userService, IQuotesService quotesService)
    {
        _quotesRepository = quotesRepository;
        _userService = userService;
        _quotesService = quotesService;
    }

    public async Task Handle(UpdateSupplyDeliveryQuotePricingCommand command,
        CancellationToken cancellationToken)
    {
        foreach (var pricing in command.Pricings)
        {
            var updateMaterialMovementRequest = new UpdateMaterialMovementRequest()
            {
                MaterialMovementId = pricing.MaterialMovementId,
                TotalMaterialPrice = pricing.MaterialPricing.TotalMaterialPrice,
                DefaultTotalMaterialPrice = pricing.MaterialPricing.DefaultTotalMaterialPrice,
                DefaultMaterialPricePerQuantityUnit = pricing.MaterialPricing.DefaultMaterialPricePerQuantityUnit,
                MaterialPricePerQuantityUnit = pricing.MaterialPricing.MaterialPricePerQuantityUnit,
            };
            await _quotesRepository.UpdateMaterialMovement(updateMaterialMovementRequest); 
            
            var updateRequest = new UpdateDeliveryMovementRequest()
            {
                DeliveryMovementId = pricing.DeliveryMovementId,
                DefaultOnewayJourneyTime = pricing.DeliveryPricing.DefaultOnewayJourneyTime,
                OnewayJourneyTime = pricing.DeliveryPricing.OnewayJourneyTime,
                DefaultTotalDeliveryPrice = pricing.DeliveryPricing.DefaultTotalDeliveryPrice,
                TotalDeliveryPrice = pricing.DeliveryPricing.TotalDeliveryPrice,
                DefaultDeliveryPricePerMinute = pricing.DeliveryPricing.DefaultDeliveryPricePerMinute,
                DeliveryPricePerMinute = pricing.DeliveryPricing.DeliveryPricePerMinute
            };
            await _quotesRepository.UpdateDeliveryMovement(updateRequest);
        }
    }
}