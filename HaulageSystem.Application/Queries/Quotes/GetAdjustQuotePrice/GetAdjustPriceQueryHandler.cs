using HaulageSystem.Application.DependencyInjection.Domain.Interfaces.Services;
using HaulageSystem.Domain.Dtos.Quotes;
using MediatR;

namespace HaulageSystem.Application.Queries.Quotes;

public class GetAdjustQuotePriceQueryHandler : IRequestHandler<GetAdjustQuotePriceQuery, QuoteAdjustPricingDto>
{
    private IQuotesService _quotesService;
    public GetAdjustQuotePriceQueryHandler(IQuotesService quotesService)
    {
        _quotesService = quotesService;
    }
    public async Task<QuoteAdjustPricingDto> Handle(GetAdjustQuotePriceQuery query, CancellationToken cancellationToken)
    {
        // return await _quotesService.GetQuotePricingInformationAsync(query.QuoteId);
        return new QuoteAdjustPricingDto();
    }
}