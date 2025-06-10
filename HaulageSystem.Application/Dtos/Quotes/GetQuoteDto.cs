using HaulageSystem.Domain.Enums;

namespace HaulageSystem.Application.Dtos.Quotes;

public class GetQuoteDto
{
    public int QuoteId { get; set; }
    public int QuoteNumber { get; set; }
    public RecordTypes QuoteType { get; set; }
    public GetQuoteCreationInfoDto CreationInfo { get; set; }
    public GetQuoteDeliveryInfoDto DeliveryInfo { get; set; }
    public List<GetQuoteMovementDto> Movements { get; set; }
    public string Comments { get; set; }
    public decimal TotalQuotePrice { get; set; }
    public string ActiveState { get; set; }
}

