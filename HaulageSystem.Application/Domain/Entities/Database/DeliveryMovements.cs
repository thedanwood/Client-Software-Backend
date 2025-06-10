using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HaulageSystem.Application.Domain.Entities.Database;

public class DeliveryMovements
{
    [Key] public int DeliveryMovementId { get; set; }

    public int QuoteId { get; set; }
    public string? StartLocationFullAddress { get; set; }
    public decimal? StartLocationLatitude { get; set; }
    public decimal? StartLocationLongitude { get; set; }
    public int NumberOfLoads { get; set; }
    public int VehicleTypeId { get; set; }
    public int DefaultOnewayJourneyTime { get; set; }
    public int OnewayJourneyTime { get; set; }
    public decimal DefaultTotalDeliveryPrice { get; set; }
    public decimal DefaultDeliveryPricePerTimeUnit { get; set; }
    public decimal TotalDeliveryPrice { get; set; }
    public decimal DeliveryPricePerTimeUnit { get; set; }
    public bool HasTrafficEnabled { get; set; }
}