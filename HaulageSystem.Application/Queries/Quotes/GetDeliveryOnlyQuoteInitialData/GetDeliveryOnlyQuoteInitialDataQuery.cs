using HaulageSystem.Application.Dtos.Quotes;
using HaulageSystem.Domain.Enums;
using MediatR;

namespace HaulageSystem.Application.Queries.Quotes;

public class GetDeliveryOnlyQuoteInitialDataQuery : IRequest<QuoteDeliveryOnlyInitialDataDto>
{
    public GetDeliveryOnlyQuoteInitialDataQuery()
    {
    }
}