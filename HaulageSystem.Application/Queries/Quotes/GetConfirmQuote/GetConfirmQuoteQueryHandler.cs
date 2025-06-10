using HaulageSystem.Application.DependencyInjection.Domain.Interfaces.Services;
using HaulageSystem.Domain.Dtos.Quotes;
using MediatR;

namespace HaulageSystem.Application.Queries.Quotes;

public class GetConfirmQuoteQueryHandler : IRequestHandler<GetConfirmQuoteQuery, int>
{
    private IQuotesService _quotesService;
    public GetConfirmQuoteQueryHandler(IQuotesService quotesService)
    {
        _quotesService = quotesService;
    }
    public async Task<int> Handle(GetConfirmQuoteQuery query, CancellationToken cancellationToken)
    {
        
        return 2;
    }
}