namespace HaulageSystem.Application.Dtos.Quotes;

public class CreateDeliveryMovementPricingDto
{
    public int DefaultOnewayJourneyTime { get; set; }
    public int OnewayJourneyTime { get; set; }
    public decimal DefaultTotalDeliveryPrice { get; set; }
    public decimal TotalDeliveryPrice { get; set; }
    public decimal DefaultDeliveryPricePerMinute { get; set; }
    public decimal DeliveryPricePerMinute { get; set; }
}