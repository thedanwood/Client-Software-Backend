namespace HaulageSystem.Application.Dtos.Quotes;

public class CreateSupplyDeliveryMovementPricingDto
{
    public decimal DefaultTotalDeliveryPrice { get; set; }
    public decimal TotalDeliveryPrice { get; set; }
    public decimal DefaultDeliveryPricePerTimeUnit { get; set; }
    public decimal DeliveryPricePerTimeUnit { get; set; }
    public decimal DefaultTotalMaterialPrice { get; set; }
    public decimal TotalMaterialPrice { get; set; }
    public decimal MaterialPricePerQuantityUnit { get; set; }
    public decimal DefaultMaterialPricePerQuantityUnit { get; set; }
}