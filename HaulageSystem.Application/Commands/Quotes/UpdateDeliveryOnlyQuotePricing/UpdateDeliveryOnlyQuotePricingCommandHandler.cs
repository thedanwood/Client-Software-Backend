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

public class UpdateDeliveryOnlyQuotePricingCommandHandler : IRequestHandler<UpdateDeliveryOnlyQuotePricingCommand>
{
    private readonly IQuotesRepository _quotesRepository;
    private readonly IQuotesService _quotesService;
    private readonly IUserService _userService;

    public UpdateDeliveryOnlyQuotePricingCommandHandler(IQuotesRepository quotesRepository,
        IUserService userService, IQuotesService quotesService)
    {
        _quotesRepository = quotesRepository;
        _userService = userService;
        _quotesService = quotesService;
    }

    public async Task Handle(UpdateDeliveryOnlyQuotePricingCommand command,
        CancellationToken cancellationToken)
    {
        foreach (var pricing in command.Pricings)
        {
            var deliveryMovement = await _quotesRepository.GetDeliveryMovementAsync(pricing.DeliveryMovementId);
            var updateRequest = new UpdateDeliveryMovementRequest()
            {
                DeliveryMovementId = deliveryMovement.DeliveryMovementId,
                DefaultOnewayJourneyTime = pricing.Pricing.DefaultOnewayJourneyTime,
                OnewayJourneyTime = pricing.Pricing.OnewayJourneyTime,
                DefaultTotalDeliveryPrice = pricing.Pricing.DefaultTotalDeliveryPrice,
                TotalDeliveryPrice = pricing.Pricing.TotalDeliveryPrice,
                DefaultDeliveryPricePerMinute = pricing.Pricing.DefaultDeliveryPricePerMinute,
                DeliveryPricePerMinute = pricing.Pricing.DeliveryPricePerMinute
            };
            await _quotesRepository.UpdateDeliveryMovement(updateRequest);
        }
    }
}