using HaulageSystem.Application.Models.Quotes;

namespace HaulageSystem.Application.Dtos.Quotes;

public class CreateDeliveryMovementDto
{
    public AddressDto StartLocation { get; set; }
    public int VehicleTypeId { get; set; }
    public CreateDeliveryMovementPricingDto Pricing { get; set; }
}