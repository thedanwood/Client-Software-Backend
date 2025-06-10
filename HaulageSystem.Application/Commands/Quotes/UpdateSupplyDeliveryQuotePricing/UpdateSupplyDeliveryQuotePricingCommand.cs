using HaulageSystem.Application.Dtos.Quotes;
using HaulageSystem.Application.Models.Quotes;
using HaulageSystem.Application.Models.Routing;
using MediatR;

namespace HaulageSystem.Application.Core.Commands.Quotes;

public class UpdateSupplyDeliveryQuotePricingCommand : IRequest
{
    public int QuoteId { get; set; }
    public List<UpdateSupplyDeliveryQuotePricingCommandItem> Pricings { get; set; }
}

public class UpdateSupplyDeliveryQuotePricingCommandItem
{
    public int MaterialMovementId { get; set; }
    public int DeliveryMovementId { get; set; }
    public UpdateDeliveryMovementPricingDto DeliveryPricing { get; set; }
    public UpdateMaterialMovementPricingDto MaterialPricing { get; set; }
}