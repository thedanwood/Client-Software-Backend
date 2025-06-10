namespace HaulageSystem.Application.Models.Requests;

public class GenerateQuotePdfRequest
{
    public string StaffFullName { get; set; }
    public string StaffEmailAddress { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public string CompanyName { get; set; }
    public string DeliveryLocationFullAddress { get; set; }
    public int QuoteId { get; set; }
    public int RecordType { get; set; }
    public string Comments { get; set; }
    public List<MovementQuotePdfRequest> Movements { get; set; }
}

public class MovementQuotePdfRequest
{
    public MovementSupplyDeliveryQuotePdfRequest SupplyDeliveryInfo { get; set; }
    public MovementDeliveryQuotePdfRequest DeliveryInfo { get; set; }
    public string VehicleTypeName { get; set; }

}

public class MovementSupplyDeliveryQuotePdfRequest
{
    public string MaterialName { get; set; }
    public decimal TotalMaterialAndDeliveryPricePerQuantityUnit { get; set; }
}
public class MovementDeliveryQuotePdfRequest
{
    public string DeliveryLocationFullAddress { get; set; }
    public string StartLocationFullAddress { get; set; }
    public decimal DeliveryPricePerQuantityUnit { get; set; }
}