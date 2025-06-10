using HaulageSystem.Application.Models.Quotes;
using HaulageSystem.Application.Models.Routing;
using MediatR;

namespace HaulageSystem.Application.Core.Commands.Quotes;

public class UpdateDeliveryOnlyQuoteCommand : IRequest
{
    public int QuoteId { get; set; }
    public string CustomerName { get; set; }
    public int CompanyId { get; set; }
    public DateTime DeliveryDateTime { get; set; }
    public AddressDto DeliveryLocation { get; set; }
    public string Comments { get; set; }
    public int NumberOfLoads { get; set; }

    public List<UpdateDeliveryMovementDto> DeliveryMovements { get; set; }
}