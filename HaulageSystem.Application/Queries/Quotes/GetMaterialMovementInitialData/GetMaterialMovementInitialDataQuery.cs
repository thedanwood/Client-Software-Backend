using HaulageSystem.Application.Domain.Dtos.Quotes;
using HaulageSystem.Domain.Enums;
using MediatR;

namespace HaulageSystem.Application.Core.Commands.Quotes;

public class GetMaterialMovementInitialDataQuery : IRequest<List<MaterialMovementForDisplayDto>>
{
    public bool HasTrafficEnabled { get; set; }
    public int MaterialId { get; set; }
    public int MaterialUnitId { get; set; }
    public decimal DeliveryLocationLatitude { get; set; }
    public decimal DeliveryLocationLongitude { get; set; }
    public int? JourneyTime { get; set; } 
}