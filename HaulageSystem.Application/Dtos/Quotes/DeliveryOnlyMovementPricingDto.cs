using HaulageSystem.Application.Models.Quotes;

namespace HaulageSystem.Application.Dtos.Quotes;

public class DeliveryOnlyMovementPricingDto
{
    public int DeliveryMovementId { get; set; }
    public decimal DefaultOnewayJourneyTime { get; set; }
    public decimal DefaultTotalDeliveryPrice { get; set; }
    public decimal TotalDeliveryPrice { get; set; }
    public decimal DefaultDeliveryPricePerTimeUnit { get; set; }
    public decimal DeliveryPricePerTimeUnit { get; set; }
}