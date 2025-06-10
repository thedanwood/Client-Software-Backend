using HaulageSystem.Application.Dtos.Quotes;
using HaulageSystem.Application.Models.Routing;
using MediatR;

namespace HaulageSystem.Application.Core.Commands.Quotes;

public class GetNewDeliveryOnlyMovementPricingQuery : IRequest<AdjustDeliveryQuotePricingDto>
{
    public int OnewayJourneyTime { get; set; }
    public RoutePoint StartLocation { get; set; }
    public RoutePoint DeliveryLocation { get; set; }
    public int NumberOfLoads { get; set; }
    public int VehicleTypeId { get; set; }
}