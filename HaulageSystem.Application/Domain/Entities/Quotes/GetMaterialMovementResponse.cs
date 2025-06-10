namespace HaulageSystem.Application.Domain.Entities.Quotes;

public class GetMaterialMovementResponse
{ 
    public int MaterialMovementId { get; set; }
    public int DeliveryMovementId { get; set; }
    public int DepotMaterialPriceId { get; set; }
    public int Quantity { get; set; }
    public int MaterialUnitId { get; set; }
    public decimal DefaultTotalMaterialPrice { get; set; }
    public decimal DefaultMaterialPricePerQuantityUnit { get; set; }
    public decimal TotalMaterialPrice { get; set; }
    public decimal MaterialPricePerQuantityUnit { get; set; }
}