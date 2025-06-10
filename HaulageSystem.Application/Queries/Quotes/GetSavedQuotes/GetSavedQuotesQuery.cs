using HaulageSystem.Application.Dtos.Quotes;
using MediatR;

namespace HaulageSystem.Application.Queries.Quotes;

public class GetSavedQuotesQuery : IRequest<List<GetQuoteDto>>
{
    // public int StartIndex { get; set; }
    // public int EndIndex { get; set; }
}