using HaulageSystem.Application.Domain.Interfaces.Repositories;
using HaulageSystem.Application.Domain.Interfaces.Services;
using HaulageSystem.Application.Helpers;
using HaulageSystem.Application.Mappers;
using HaulageSystem.Domain.Enums;
using MediatR;

namespace HaulageSystem.Application.Commands.MaterialPricing;

public class CreateMaterialPricingsCommandHandler : IRequestHandler<CreateMaterialPricingsCommand>
{
    private readonly IMaterialPricingRepository _materialPricingRepository;
    private readonly IUserService _userService;

    public CreateMaterialPricingsCommandHandler(IMaterialPricingRepository materialPricingRepository, IUserService userService)
    {
        _materialPricingRepository = materialPricingRepository;
        _userService = userService;
    }

    public async Task Handle(CreateMaterialPricingsCommand command, CancellationToken cancellationToken)
    {
        var username = await _userService.GetCurrentUsername();
        foreach (var price in command.Prices.Where(x=>x.Price != null))
        {
            await _materialPricingRepository.CreateMaterialPricingAsync(new()
            {
                MaterialId = command.MaterialId,
                DepotId = command.DepotId,
                Price = price.Price.Value,
                UnitId = price.UnitId,
                IsActive = command.IsActive,
                ActiveState = DepotMaterialPricesActiveStates.Active.ToInt(),
                CreatedByUsername = username
            });
        }
    }
}