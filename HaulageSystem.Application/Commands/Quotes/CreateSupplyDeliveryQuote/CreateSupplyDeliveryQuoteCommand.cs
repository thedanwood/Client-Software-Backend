using HaulageSystem.Application.Dtos.Materials;
using HaulageSystem.Application.Dtos.Quotes;
using HaulageSystem.Application.Models.Quotes;
using HaulageSystem.Domain.Enums;
using MediatR;

namespace HaulageSystem.Application.Core.Commands.Quotes;

public class CreateSupplyDeliveryQuoteCommand : IRequest<int>
{
    public string? CustomerName { get; set; }
    public int CompanyId { get; set; }
    public AddressDto DeliveryLocation { get; set; }
    public DateTime? DeliveryDateTime { get; set; }
    public string? Comments { get; set; }
    public List<CreateMaterialMovementCommand> MaterialMovements { get; set; }
}