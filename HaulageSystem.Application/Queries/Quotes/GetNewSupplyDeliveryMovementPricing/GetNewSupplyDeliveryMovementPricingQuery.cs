using HaulageSystem.Application.Dtos.Quotes;
using HaulageSystem.Application.Models.Routing;
using MediatR;

namespace HaulageSystem.Application.Core.Commands.Quotes;

public class GetNewSupplyDeliveryMovementPricingQuery : IRequest<SupplyDeliveryMovementPricingDto>
{
    public RoutePoint DeliveryLocation { get; set; }
    public int OnewayJourneyTime { get; set; }
    public int Quantity { get; set; }
    public int NumberOfLoads { get; set; }
    public int VehicleTypeId { get; set; }
    public int MaterialUnitId { get; set; }
    public int DepotMaterialPriceId { get; set; }
}