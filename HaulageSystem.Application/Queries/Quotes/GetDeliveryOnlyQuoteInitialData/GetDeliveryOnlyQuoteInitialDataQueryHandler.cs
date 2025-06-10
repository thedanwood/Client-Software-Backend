using HaulageSystem.Application.Domain.Dtos.Materials;
using HaulageSystem.Application.Domain.Dtos.Vehicles;
using HaulageSystem.Application.Domain.Interfaces.Repositories;
using HaulageSystem.Application.Domain.Interfaces.Services;
using HaulageSystem.Application.Dtos.Quotes;
using MediatR;

namespace HaulageSystem.Application.Queries.Quotes;

public class GetDeliveryOnlyQuoteInitialDataQueryHandler : IRequestHandler<GetDeliveryOnlyQuoteInitialDataQuery, QuoteDeliveryOnlyInitialDataDto>
{
    private readonly IVehiclesRepository _vehiclesRepository;
    private readonly IProfileService _profileService;
    public GetDeliveryOnlyQuoteInitialDataQueryHandler(IVehiclesRepository vehiclesRepository, IProfileService profileService)
    {
        _vehiclesRepository = vehiclesRepository;
        _profileService = profileService;
    }
    public async Task<QuoteDeliveryOnlyInitialDataDto> Handle(GetDeliveryOnlyQuoteInitialDataQuery query, CancellationToken cancellationToken)
    {
        var vehicleTypes = await _vehiclesRepository.GetAllVehicleTypes();
        return new QuoteDeliveryOnlyInitialDataDto()
        {
            VehicleTypes = vehicleTypes.Select(x => new VehicleTypeDto(x.Id, x.Name, x.Capacity)).ToList(),
            DefaultHasTrafficEnabled = _profileService.GetDefaultHasTrafficEnabled()
        };
    }
}