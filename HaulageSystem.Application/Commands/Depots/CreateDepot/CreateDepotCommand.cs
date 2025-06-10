using HaulageSystem.Application.Models.Quotes;
using MediatR;

namespace HaulageSystem.Application.Commands.Depots.CreateDepot;

public class CreateDepotCommand : IRequest<int>
{
    public string DepotName { get; set; }
    public AddressDto Address { get; set; }
    public bool IsActive { get; set; }
}