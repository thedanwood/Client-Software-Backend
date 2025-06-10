using HaulageSystem.Application.Dtos.Quotes;
using HaulageSystem.Domain.Enums;

namespace HaulageSystem.Application.Dtos.Materials;

public class CreateMaterialMovementCommand
{
    public int OnewayJourneyTime { get; set; }
    public int DepotMaterialPriceId { get; set; }
    public int Quantity { get; set; }
    public int MaterialUnitId { get; set; }
    public int VehicleTypeId { get; set; }
    public bool HasTrafficEnabled { get; set; }
    public CreateSupplyDeliveryMovementPricingDto Pricing { get; set; }
}