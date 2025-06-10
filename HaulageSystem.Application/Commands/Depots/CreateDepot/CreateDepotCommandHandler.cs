using HaulageSystem.Application.Domain.Interfaces.Repositories;
using MediatR;

namespace HaulageSystem.Application.Commands.Depots.CreateDepot;

public class CreateDepotCommandHandler : IRequestHandler<CreateDepotCommand, int>
{
    private readonly IDepotsRepository _depotsRepository;

    public CreateDepotCommandHandler(IDepotsRepository depotsRepository)
    {
        _depotsRepository = depotsRepository ?? throw new ArgumentNullException(nameof(depotsRepository));
    }

    public async Task<int> Handle(CreateDepotCommand request, CancellationToken cancellationToken)
    {
        var depotId = await _depotsRepository.CreateDepot(request.DepotName, request.Address.AddressPoint.Latitude,
            request.Address.AddressPoint.Longitude, request.Address.FullAddress, request.IsActive);
        return depotId;
    }
}