using HaulageSystem.Application.Domain.Dtos.Materials;
using HaulageSystem.Domain.Enums;
using MediatR;

namespace HaulageSystem.Application.Core.Commands.Quotes;

public class GetDeliveryOnlyMovementInitialDataQuery : IRequest<DeliveryOnlyMovementForDisplayDto>
{
    public decimal DeliveryLocationLatitude { get; set; }
    public decimal DeliveryLocationLongitude { get; set; }
    public decimal StartLocationLongitude { get; set; }
    public decimal StartLocationLatitude { get; set; }
}