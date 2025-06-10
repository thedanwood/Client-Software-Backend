using HaulageSystem.Application.Models.Quotes;
using MediatR;

namespace HaulageSystem.Application.Core.Commands.Depots;

public class UpdateDepotCommand : IRequest
{
    public int DepotId { get; set; }
    public string DepotName { get; set; }
    public AddressDto Address { get; set; }
    public bool IsActive { get; set; }
}