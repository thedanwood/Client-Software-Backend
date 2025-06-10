using HaulageSystem.Application.Core.Commands.Materials;
using HaulageSystem.Application.Domain.Interfaces.Repositories;
using HaulageSystem.Application.Domain.Interfaces.Services;
using HaulageSystem.Application.Domain.Services;
using HaulageSystem.Application.Exceptions;
using MediatR;
using OneOf.Types;

namespace HaulageSystem.Application.Commands.Materials;

public class DeleteMaterialCommandHandler : IRequestHandler<DeleteMaterialsCommand>
{
    private readonly IMaterialsRepository _materialsRepository;
    private readonly IUserService _userService;

    public DeleteMaterialCommandHandler(IMaterialsRepository materialsRepository, IUserService userService)
    {
        _materialsRepository = materialsRepository;
        _userService = userService;
    }

    public async Task Handle(DeleteMaterialsCommand command, CancellationToken cancellationToken)
    {
        await _materialsRepository.DeleteMaterialsAsync(command.MaterialIds);
    }
}