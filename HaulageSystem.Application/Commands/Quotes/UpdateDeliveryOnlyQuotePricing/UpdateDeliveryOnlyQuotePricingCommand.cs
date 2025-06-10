using HaulageSystem.Application.Dtos.Quotes;
using HaulageSystem.Application.Models.Quotes;
using HaulageSystem.Application.Models.Routing;
using MediatR;

namespace HaulageSystem.Application.Core.Commands.Quotes;

public class UpdateDeliveryOnlyQuotePricingCommand : IRequest
{
    public int QuoteId { get; set; }
    public List<UpdateDeliveryOnlyQuotePricingCommandItem> Pricings { get; set; }
}

public class UpdateDeliveryOnlyQuotePricingCommandItem
{
    public int DeliveryMovementId { get; set; }
    public UpdateDeliveryMovementPricingDto Pricing { get; set; }
}