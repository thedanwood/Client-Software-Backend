using HaulageSystem.Application.Models.Quotes;

namespace HaulageSystem.Application.Dtos.Quotes;

public class AdjustDeliveryQuotePricingDto
{
    public decimal DefaultOnewayJourneyTime { get; set; }
    public decimal DefaultTotalDeliveryPrice { get; set; }
    public decimal TotalDeliveryPrice { get; set; }
    public decimal DefaultDeliveryPricePerTimeUnit { get; set; }
    public decimal DeliveryPricePerTimeUnit { get; set; }
    public string DeliveryUnitName { get; set; }
    public string VehicleTypeName { get; set; }
    public int NumberOfLoads { get; set; }
}