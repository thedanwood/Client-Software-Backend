using HaulageSystem.Application.Dtos.Quotes;
using HaulageSystem.Application.Models.Quotes;
using MediatR;

namespace HaulageSystem.Application.Core.Commands.Quotes;

public class CreateDeliveryOnlyQuoteCommand : IRequest<int>
{
    public string CustomerName { get; set; }
    public int CompanyId { get; set; }
    public AddressDto DeliveryLocation { get; set; }
    public DateTime DeliveryDateTime { get; set; }
    public string Comments { get; set; }
    public int NumberOfLoads { get; set; }
    public List<CreateDeliveryMovementDto> DeliveryMovements { get; set; }
}