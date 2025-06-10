using HaulageSystem.Application.Dtos.Quotes;
using HaulageSystem.Domain.Enums;
using MediatR;

namespace HaulageSystem.Application.Queries.Quotes;

public class GetSupplyDeliveryUpdateQuoteInitialDataQuery : IRequest<UpdateQuoteSupplyDeliveryInitialDataDto>
{
    public GetSupplyDeliveryUpdateQuoteInitialDataQuery(int quoteId)  
    {
        QuoteId = quoteId;
    }
    public int QuoteId { get; set; }
}