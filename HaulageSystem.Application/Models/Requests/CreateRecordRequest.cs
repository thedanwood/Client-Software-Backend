using HaulageSystem.Application.Domain.Entities.Database;

namespace HaulageSystem.Application.Models.Requests;

public class CreateRecordRequest
{
    public int CompanyId { get; set; }
    public string CreatedByUsername { get; set; }
    public string CustomerName { get; set; }
    public string DeliveryLocationFullAddress { get; set; }
    public decimal DeliveryLocationLongitude { get; set; }
    public decimal DeliveryLocationLatitude { get; set; }
    public DateTime? DeliveryDate { get; set; }
    public int ActiveStateEnumValue { get; set; }
    public string Comments { get; set; }
    public int RecordType { get; set; }
    public int RecordVariation { get; set; }
}