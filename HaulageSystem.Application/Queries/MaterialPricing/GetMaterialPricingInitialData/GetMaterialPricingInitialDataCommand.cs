using HaulageSystem.Application.Dtos.Quotes;
using MediatR;

namespace HaulageSystem.Application.Commands.MaterialPricing;

public class GetMaterialPricingInitialDataCommand : IRequest<MaterialPricingInitialDataDto>
{
    public int DepotId { get; set; }
}