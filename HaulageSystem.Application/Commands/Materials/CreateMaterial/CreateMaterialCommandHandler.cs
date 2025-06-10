using HaulageSystem.Application.Core.Commands.Materials;
using HaulageSystem.Application.Core.Commands.Quotes;
using HaulageSystem.Application.Domain.Interfaces.Repositories;
using HaulageSystem.Application.Domain.Interfaces.Services;
using HaulageSystem.Application.Mappers;
using HaulageSystem.Domain.Interfaces;
using MediatR;

namespace HaulageSystem.Application.Commands.Materials;

public class CreateMaterialCommandHandler : IRequestHandler<CreateMaterialCommand, int>
{
    private readonly IMaterialsRepository _materialsRepository;

    public CreateMaterialCommandHandler(IMaterialsRepository materialsRepository)
    {
        _materialsRepository = materialsRepository;
    }

    public async Task<int> Handle(CreateMaterialCommand command, CancellationToken cancellationToken)
    {
        var materialId = await _materialsRepository.CreateMaterialAsync(command.MaterialName);
        return materialId;
    }
}