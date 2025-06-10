using HaulageSystem.Application.Domain.Dtos.Materials;
using HaulageSystem.Application.Dtos.Quotes;
using MediatR;

namespace HaulageSystem.Application.Core.Commands.Companies;

public class EmailQuoteCommand : IRequest<bool>
{
    public string CompanyEmail { get; set; }
    public int QuoteId {
        get;
        set;
    }
}