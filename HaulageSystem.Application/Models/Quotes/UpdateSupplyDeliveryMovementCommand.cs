using HaulageSystem.Application.Dtos.Quotes;

namespace HaulageSystem.Application.Models.Quotes;

public class UpdateSupplyDeliveryMovementCommand
{
    public int? MaterialMovementId { get; set; }
    public int VehicleTypeId { get; set; }
    public int NumberOfLoads { get; set; }
    public int Quantity { get; set; }
    public int DepotMaterialPriceId { get; set; }
    public int MaterialUnitId { get; set; }
    public bool HasTrafficEnabled { get; set; }
    public UpdateDeliveryMovementPricingDto DeliveryPricing { get; set; }
    public UpdateMaterialMovementPricingDto MaterialPricing { get; set; }
}