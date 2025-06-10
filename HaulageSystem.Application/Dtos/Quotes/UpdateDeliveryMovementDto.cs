using HaulageSystem.Application.Dtos.Quotes;

namespace HaulageSystem.Application.Models.Quotes;

public class UpdateDeliveryMovementDto
{
    public int? DeliveryMovementId { get; set; }
    public AddressDto StartLocation { get; set; }

    public int VehicleTypeId { get; set; }
    public UpdateDeliveryMovementPricingDto Pricing { get; set; }
}