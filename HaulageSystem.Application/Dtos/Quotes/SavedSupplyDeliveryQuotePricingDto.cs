namespace HaulageSystem.Application.Dtos.Quotes;


public class SavedSupplyDeliveryQuotePricingDto
{
    public int DeliveryMovementId { get; set; }
    public int DefaultOnewayJourneyTime { get; set; }
    public int OnewayJourneyTime { get; set; }
    public decimal MaterialAndDeliveryPricePerQuantityUnit { get; set; }
    public decimal DefaultMaterialAndDeliveryPricePerQuantityUnit { get; set; }
    public SavedSupplyDeliveryQuoteMaterialPricingDto MaterialPricing { get; set; }
    public SavedSupplyDeliveryQuoteDeliveryPricingDto DeliveryPricing { get; set; }

}

public class SavedSupplyDeliveryQuoteDeliveryPricingDto
{
    public decimal DefaultTotalDeliveryPrice { get; set; }
    public decimal TotalDeliveryPrice { get; set; }
    public decimal DefaultDeliveryPricePerTimeUnit { get; set; }
    public decimal DeliveryPricePerTimeUnit { get; set; }
}

public class SavedSupplyDeliveryQuoteMaterialPricingDto
{
    public int MaterialMovementId { get; set; }
    public decimal TotalMaterialPrice { get; set; }
    public decimal DefaultTotalMaterialPrice { get; set; }
    public decimal MaterialPricePerQuantityUnit { get; set; }
    public decimal DefaultMaterialPricePerQuantityUnit { get; set; }
    public int NumberOfLoads { get; set; }
}
