using HaulageSystem.Application.Core.Commands.Quotes;
using HaulageSystem.Application.Domain.Interfaces.Repositories;
using HaulageSystem.Application.Domain.Interfaces.Services;
using HaulageSystem.Application.Helpers;
using HaulageSystem.Domain.Enums;
using MediatR;

namespace HaulageSystem.Application.Commands.Quotes;

public class DeleteQuoteCommandHandler : IRequestHandler<DeleteQuoteCommand>
{
    private readonly IQuotesRepository _quotesRepository;

    public DeleteQuoteCommandHandler(IQuotesRepository quotesRepository)
    {
        _quotesRepository = quotesRepository;
    }

    public async Task Handle(DeleteQuoteCommand command,
        CancellationToken cancellationToken)
    {
        await _quotesRepository.SetActiveState(command.QuoteId, QuoteActiveStates.ManuallyArchived.ToInt());
    }
}