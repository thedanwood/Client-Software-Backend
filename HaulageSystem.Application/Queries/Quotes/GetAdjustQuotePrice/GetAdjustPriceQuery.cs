using HaulageSystem.Domain.Dtos.Quotes;
using HaulageSystem.Domain.Enums;
using MediatR;

namespace HaulageSystem.Application.Queries.Quotes;

public class GetAdjustQuotePriceQuery : IRequest<QuoteAdjustPricingDto>
{
    public int QuoteId { get; set; }
}