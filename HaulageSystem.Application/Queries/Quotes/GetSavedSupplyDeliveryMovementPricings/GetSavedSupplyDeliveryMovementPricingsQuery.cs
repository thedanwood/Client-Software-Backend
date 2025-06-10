using HaulageSystem.Application.Domain.Services;
using HaulageSystem.Application.Dtos.Quotes;
using HaulageSystem.Application.Models.Routing;
using MediatR;

namespace HaulageSystem.Application.Core.Commands.Quotes;

public class GetSavedSupplyDeliveryMovementPricingsQuery : IRequest<List<SupplyDeliveryMovementPricingDto>>
{
    public int QuoteId { get; set; }
    
}