using HaulageSystem.Application.Dtos.Quotes;
using MediatR;

namespace HaulageSystem.Application.Queries.Quotes;

public class GetPastQuotesQuery : IRequest<List<GetQuoteDto>>
{
    // Add your query parameters here
}