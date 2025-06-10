using HaulageSystem.Application.Domain.Dtos.Vehicles;
using HaulageSystem.Application.Models.Quotes;

namespace HaulageSystem.Application.Dtos.Quotes;

public class GetQuoteDeliveryMovementDto
{
    public int DeliveryMovementId { get; set; }
    public VehicleTypeDto VehicleType { get; set; }
    public int NumberOfLoads { get; set; }
    public int OnewayJourneyTime { get; set; }
    public AddressDto StartLocation { get; set; }
}