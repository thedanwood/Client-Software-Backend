using HaulageSystem.Application.Domain.Entities.Database;

namespace HaulageSystem.Application.Models.Requests;

public class CreateDeliveryMovementRequest
{
    public int QuoteId { get; set; }
    public string StartLocationFullAddress { get; set; }
    public decimal StartLocationLatitude { get; set; }
    public decimal StartLocationLongitude { get; set; }
    public int NumberOfLoads { get; set; }
    public int VehicleTypeId { get; set; }
    public int DefaultOnewayJourneyTime { get; set; }
    public int OnewayJourneyTime { get; set; }
    public decimal DefaultTotalDeliveryPrice { get; set; }
    public decimal DefaultDeliveryPricePerDeliveryTimeUnit { get; set; }
    public decimal TotalDeliveryPrice { get; set; }
    public decimal DeliveryPricePerDeliveryTimeUnit { get; set; }
    public bool HasTrafficEnabled { get; set; }
}