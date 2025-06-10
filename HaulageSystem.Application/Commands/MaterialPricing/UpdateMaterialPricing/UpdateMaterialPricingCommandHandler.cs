using HaulageSystem.Application.Domain.Interfaces.Repositories;
using HaulageSystem.Application.Domain.Interfaces.Services;
using HaulageSystem.Application.Exceptions;
using HaulageSystem.Application.Mappers;
using MediatR;
using OneOf.Types;
using System.Runtime.CompilerServices;
using HaulageSystem.Application.Helpers;
using HaulageSystem.Application.Models.Requests;
using HaulageSystem.Domain.Enums;

namespace HaulageSystem.Application.Commands.MaterialPricing;

public class UpdateMaterialPricingCommandHandler : IRequestHandler<UpdateMaterialPricingCommand>
{
    private readonly IMaterialPricingRepository _materialsPricingRepository;
    private readonly IUserService _userService;

    public UpdateMaterialPricingCommandHandler(IMaterialPricingRepository materialsPricingRepository,
        IUserService userService)
    {
        _materialsPricingRepository = materialsPricingRepository;
        _userService = userService;
    }

    public async Task Handle(UpdateMaterialPricingCommand command, CancellationToken cancellationToken)
    {
        var username = await _userService.GetCurrentUsername();

        foreach (var pricing in command.Pricings)
        {
            if (ShouldDeletePricing(pricing))
            {
                await DeleteExistingPricing(pricing, username);
                return;
            }

            if (ShouldUpdatePricing(pricing))
            {
                await UpdateExistingPricing(pricing, command, username);
                return;
            }

            await CreateNewPricing(pricing, command, username);
        }
    }

    private static bool ShouldDeletePricing(UpdateMaterialPricingItemCommand pricing)
        => pricing.Price == null && pricing.DepotMaterialPriceId.HasValue;

    private static bool ShouldUpdatePricing(UpdateMaterialPricingItemCommand pricing)
        => pricing.Price.HasValue && pricing.DepotMaterialPriceId.HasValue;

    private async Task DeleteExistingPricing(UpdateMaterialPricingItemCommand pricing, string username)
    {
        await _materialsPricingRepository.DeleteMaterialPricingsAsync(
            new List<int> { pricing.DepotMaterialPriceId!.Value });
    }

    private async Task UpdateExistingPricing(
        UpdateMaterialPricingItemCommand pricing,
        UpdateMaterialPricingCommand command,
        string username)
    {
        await _materialsPricingRepository.UpdateMaterialPricingActiveStateAsync(pricing.DepotMaterialPriceId.Value,
            DepotMaterialPricesActiveStates.Replaced.ToInt());
        await CreateNewPricing(pricing, command, username);
    }

    private async Task CreateNewPricing(
        UpdateMaterialPricingItemCommand pricing,
        UpdateMaterialPricingCommand command,
        string username)
    {
        var createModel = new CreateMaterialPricingRequest()
        {
            Price = pricing.Price!.Value,
            MaterialId = command.MaterialId,
            UnitId = pricing.UnitId,
            IsActive = true,
            ActiveState = DepotMaterialPricesActiveStates.Active.ToInt(),
            DepotId = command.DepotId,
            CreatedByUsername = username
        };

        await _materialsPricingRepository.CreateMaterialPricingAsync(createModel);
    }
}