namespace HaulageSystem.Application.Dtos.Quotes;

public class AdjustQuotePricingDto
{
    public AdjustDeliveryQuotePricingDto DeliveryPricing { get; set; }
    public AdjustMaterialQuotePriceDto MaterialPricing { get; set; }
    public decimal DefaultMaterialAndDeliveryPricePerQuantityUnit { get; set; }
    public decimal MaterialAndDeliveryPricePerQuantityUnit { get; set; }
}