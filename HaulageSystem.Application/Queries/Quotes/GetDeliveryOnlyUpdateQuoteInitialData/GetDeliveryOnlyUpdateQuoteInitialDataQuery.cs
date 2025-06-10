using HaulageSystem.Application.Dtos.Quotes;
using HaulageSystem.Domain.Enums;
using MediatR;

namespace HaulageSystem.Application.Queries.Quotes;

public class GetDeliveryOnlyUpdateQuoteInitialDataQuery : IRequest<UpdateQuoteDeliveryOnlyInitialDataDto>
{
    public GetDeliveryOnlyUpdateQuoteInitialDataQuery(int quoteId)  
    {
        QuoteId = quoteId;
    }
    public int QuoteId { get; set; }
}