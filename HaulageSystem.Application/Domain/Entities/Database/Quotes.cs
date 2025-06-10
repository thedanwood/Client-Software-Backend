using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HaulageSystem.Application.Domain.Entities.Database;

public class Quotes 
{
    [Key] 
    public int QuoteId { get; set; }
    public string CreatedByUsername { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public int CompanyId { get; set; }
    public string? CustomerName { get; set; }
    public string DeliveryLocationFullAddress { get; set; }
    public decimal DeliveryLocationLongitude { get; set; }
    public decimal DeliveryLocationLatitude { get; set; }
    public DateTime? DeliveryDate { get; set; }
    public int ActiveStateEnumValue { get; set; }
    public string? Comments { get; set; }
    public int RecordType { get; set; }
}