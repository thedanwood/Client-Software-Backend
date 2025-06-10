namespace HaulageSystem.Application.Models.Quotes;

public class ConfirmQuoteDeliveryMovementQuery
{
    public AddressDto DeliveryLocation { get; set; }
    public AddressDto StartLocation { get; set; }
}