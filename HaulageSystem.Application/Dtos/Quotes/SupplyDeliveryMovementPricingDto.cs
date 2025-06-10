using HaulageSystem.Application.Models.Quotes;

namespace HaulageSystem.Application.Dtos.Quotes;

public class SupplyDeliveryMovementPricingDto
{
    public int DeliveryMovementId { get; set; }
    public int MaterialMovementId { get; set; }
    public decimal DefaultOnewayJourneyTime { get; set; }
    public decimal DefaultTotalDeliveryPrice { get; set; }
    public decimal TotalDeliveryPrice { get; set; }
    public decimal DefaultDeliveryPricePerTimeUnit { get; set; }
    public decimal DeliveryPricePerTimeUnit { get; set; }
    public decimal DefaultMaterialPricePerQuantityUnit { get; set; }
    public decimal MaterialPricePerQuantityUnit { get; set; }
    public decimal DefaultMaterialAndDeliveryPricePerQuantityUnit { get; set; }
    public decimal MaterialAndDeliveryPricePerQuantityUnit { get; set; }
    public decimal DefaultTotalMaterialPrice { get; set; }
    public decimal TotalMaterialPrice { get; set; }
}