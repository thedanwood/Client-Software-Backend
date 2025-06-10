using HaulageSystem.Application.Core.Commands.Materials;
using HaulageSystem.Application.Domain.Interfaces.Repositories;
using HaulageSystem.Application.Domain.Interfaces.Services;
using HaulageSystem.Application.Exceptions;
using MediatR;
using OneOf.Types;

namespace HaulageSystem.Application.Commands.Materials;

public class UpdateMaterialCommandHandler : IRequestHandler<UpdateMaterialCommand>
{
    private readonly IMaterialsRepository _materialsRepository;
    private readonly IUserService _userService;

    public UpdateMaterialCommandHandler(IMaterialsRepository materialsRepository, IUserService userService)
    {
        _materialsRepository = materialsRepository;
        _userService = userService;
    }

    public async Task Handle(UpdateMaterialCommand command, CancellationToken cancellationToken)
    {
        var username = await _userService.GetCurrentUsername();
        await _materialsRepository.UpdateMaterialAsync(command.MaterialId, command.MaterialName);
    }
}