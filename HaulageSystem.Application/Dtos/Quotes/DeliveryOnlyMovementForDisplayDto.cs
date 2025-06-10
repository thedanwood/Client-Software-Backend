using HaulageSystem.Application.Dtos.Quotes;
using HaulageSystem.Domain.Enums;

namespace HaulageSystem.Application.Domain.Dtos.Materials;

public class DeliveryOnlyMovementForDisplayDto
{
    public List<JouneyTimeHasTrafficDto> JourneyTimes { get; set; }
}