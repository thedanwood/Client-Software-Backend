using HaulageSystem.Domain.Enums;

namespace HaulageSystem.Application.Models.Quotes;

public class ConfirmQuoteMaterialMovementQuery
{
    public int Quantity { get; set; }
    public int MaterialUnitId { get; set; }
    public int DepotMaterialPriceId { get; set; }
    public int VehicleTypeId { get; set; }
    public AddressDto DeliveryLocation { get; set; }
}