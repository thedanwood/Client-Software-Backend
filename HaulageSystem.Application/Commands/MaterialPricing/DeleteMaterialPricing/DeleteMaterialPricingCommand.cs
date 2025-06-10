using MediatR;

namespace HaulageSystem.Application.Commands.MaterialPricing;

public class DeleteMaterialPricingCommand : IRequest
{
    public List<int> DepotMaterialPriceIds { get; set; }
}