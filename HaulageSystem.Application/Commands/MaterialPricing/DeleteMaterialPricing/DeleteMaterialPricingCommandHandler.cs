using HaulageSystem.Application.Domain.Interfaces.Repositories;
using HaulageSystem.Application.Domain.Interfaces.Services;
using HaulageSystem.Application.Exceptions;
using HaulageSystem.Application.Mappers;
using HaulageSystem.Application.Models.Requests;
using HaulageSystem.Domain.Interfaces;
using MediatR;
using OneOf.Types;

namespace HaulageSystem.Application.Commands.MaterialPricing;

public class DeleteMaterialPricingCommandHandler : IRequestHandler<DeleteMaterialPricingCommand>
{
    private readonly IMaterialPricingRepository _materialsPricingRepository;
    private readonly IUserService _userService;

    public DeleteMaterialPricingCommandHandler(IMaterialPricingRepository materialsPricingRepository, IUserService userService)
    {
        _materialsPricingRepository = materialsPricingRepository;
        _userService = userService;
    }

    public async Task Handle(DeleteMaterialPricingCommand command, CancellationToken cancellationToken)
    {
        await _materialsPricingRepository.DeleteMaterialPricingsAsync(command.DepotMaterialPriceIds);
    }
}