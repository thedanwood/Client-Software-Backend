using HaulageSystem.Application.Core.Commands.Materials;
using HaulageSystem.Application.Domain.Interfaces.Repositories;
using HaulageSystem.Application.Domain.Interfaces.Services;
using HaulageSystem.Application.Exceptions;
using MediatR;
using OneOf.Types;

namespace HaulageSystem.Application.Commands.Materials;

public class DeleteDepotsCommandHandler : IRequestHandler<DeleteDepotsCommand>
{
    private readonly IDepotsRepository _depotsRepository;
    private readonly IUserService _userService;
    public DeleteDepotsCommandHandler(IDepotsRepository depotsRepository, IUserService userService)
    {
        _depotsRepository = depotsRepository ?? throw new ArgumentNullException(nameof(depotsRepository));
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
    }

    public async Task Handle(DeleteDepotsCommand command, CancellationToken cancellationToken)
    {
        await _depotsRepository.DeleteDepots(command.DepotIds);
    }
}