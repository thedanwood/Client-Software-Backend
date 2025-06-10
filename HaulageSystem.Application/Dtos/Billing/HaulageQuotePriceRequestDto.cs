using HaulageSystem.Application.Models.Routing;

namespace HaulageSystem.Application.Dtos.Quotes;

public class HaulageQuotePriceRequestDto
{
    public RoutePoint StartLocation { get; set; }
    public RoutePoint DeliveryLocation { get; set; }
    public int NumberOfLoads { get; set; }
    public int VehicleTypeId { get; set; }
    public int OnewayJourneyTime { get; set; }
}