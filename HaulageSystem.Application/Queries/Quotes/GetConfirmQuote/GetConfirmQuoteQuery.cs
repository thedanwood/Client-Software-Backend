using HaulageSystem.Domain.Dtos.Quotes;
using HaulageSystem.Domain.Enums;
using MediatR;

namespace HaulageSystem.Application.Queries.Quotes;

public class GetConfirmQuoteQuery : IRequest<int>
{
    public int OrderId { get; set; }
}