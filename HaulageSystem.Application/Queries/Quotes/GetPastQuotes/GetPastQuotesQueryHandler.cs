using System.Data;
using HaulageSystem.Application.Domain.Interfaces.Repositories;
using HaulageSystem.Application.Domain.Interfaces.Services;
using HaulageSystem.Application.Extensions;
using HaulageSystem.Domain.Interfaces;
using HaulageSystem.Application.Dtos.Quotes;
using HaulageSystem.Application.Helpers;
using HaulageSystem.Domain.Enums;
using MediatR;

namespace HaulageSystem.Application.Queries.Quotes;

public class GetPastQuotesQueryHandler : IRequestHandler<GetPastQuotesQuery, List<GetQuoteDto>>
{
    private readonly IQuotesRepository _quoteRepository;
    private readonly IUserService _userService;
    private readonly IQuotesService _quotesService;

    public GetPastQuotesQueryHandler(IQuotesRepository quotesRepository, IUserService userService, IQuotesService quotesService)
    {
        _quoteRepository = quotesRepository;
        _userService = userService;
        _quotesService = quotesService;
    }

    public async Task<List<GetQuoteDto>> Handle(GetPastQuotesQuery query, CancellationToken cancellationToken)
    {
        var quotes = await _quoteRepository.GetQuotes(90);

        var dtos = new List<GetQuoteDto>();
        foreach (var quote in quotes)
        {
            var creationInfo = $"Created by {await _userService.GetFullName()} at {quote.CreatedDateTime.ToDateTimeString()}";
            
            var dto = new GetQuoteDto()
            {
                QuoteId = quote.QuoteId,
                QuoteNumber = quote.QuoteId,
                // CreationInfo = creationInfo,
                // DeliveryInfo = await _quotesService.GetQuoteDeliveryInformationText(quote),
                Comments = quote.Comments,
                // TotalPrice = await _quotesService.GetTotalQuotePrice(quote),
                ActiveState = EnumHelpers.GetActiveStateDisplayableText(quote.ActiveStateEnumValue)
            };
            dtos.Add(dto);
        }

        return dtos;
    }
}