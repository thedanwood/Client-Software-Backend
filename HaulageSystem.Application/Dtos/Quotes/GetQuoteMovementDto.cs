namespace HaulageSystem.Application.Dtos.Quotes;

public class GetQuoteMovementDto
{
    public GetQuoteMaterialMovementDto MaterialMovement { get; set; }
    public GetQuoteDeliveryMovementDto DeliveryMovement { get; set; }
}