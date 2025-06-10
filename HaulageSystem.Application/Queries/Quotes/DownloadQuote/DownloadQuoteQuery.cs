using HaulageSystem.Application.Domain.Dtos.Materials;
using HaulageSystem.Application.Dtos.Quotes;
using MediatR;

namespace HaulageSystem.Application.Core.Commands.Quotes;

public class DownloadQuoteQuery : IRequest<DownloadQuotePdfDto>
{
    public int QuoteId {
        get;
        set;
    }
}