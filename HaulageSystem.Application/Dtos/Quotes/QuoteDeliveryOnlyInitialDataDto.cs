using HaulageSystem.Application.Domain.Dtos.Materials;
using HaulageSystem.Application.Domain.Dtos.Vehicles;

namespace HaulageSystem.Application.Dtos.Quotes;

public class QuoteDeliveryOnlyInitialDataDto
{
    public List<VehicleTypeDto> VehicleTypes { get; set; }
    public bool DefaultHasTrafficEnabled { get; set; }
}