using HaulageSystem.Application.Core.Commands.Depots;
using HaulageSystem.Application.Domain.Interfaces.Repositories;
using HaulageSystem.Application.Domain.Interfaces.Services;
using HaulageSystem.Application.Domain.Services;
using MediatR;

namespace HaulageSystem.Application.Commands.Depots;

public class UpdateDepotCommandHandler : IRequestHandler<UpdateDepotCommand>
{
    private readonly IUserService _userService;
    private readonly IDepotsRepository _depotsRepository;

    public UpdateDepotCommandHandler(IDepotsRepository depotsRepository, IUserService userService)
    {
        _depotsRepository = depotsRepository;
        _userService = userService;
    }

    public async Task Handle(UpdateDepotCommand command, CancellationToken cancellationToken)
    {
        var currentUsername = await _userService.GetCurrentUsername();
        await _depotsRepository.UpdateDepot(new()
        {
            DepotId = command.DepotId,
            DepotName = command.DepotName,
            IsActive = command.IsActive,
            FullAddress = command.Address.FullAddress,
            Latitude= command.Address.AddressPoint.Latitude,
            Longitude= command.Address.AddressPoint.Longitude,
            UpdatedByUsername = currentUsername,
        });
    }
}