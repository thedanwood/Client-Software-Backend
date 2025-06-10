using HaulageSystem.Application.Models.Quotes;
using MediatR;

namespace HaulageSystem.Application.Core.Commands.Quotes;

public class UpdateSupplyDeliveryQuoteCommand : IRequest
{
    public int QuoteId { get; set; }
    public string CustomerName { get; set; }
    public int CompanyId { get; set; }
    public DateTime DeliveryDateTime { get; set; }
    public AddressDto DeliveryLocation { get; set; }
    public string Comments { get; set; }

    public List<UpdateSupplyDeliveryMovementCommand> Movements { get; set; }
}