using MediatR;

namespace HaulageSystem.Application.Commands.MaterialPricing;

public class UpdateMaterialPricingCommand : IRequest
{
    public List<UpdateMaterialPricingItemCommand> Pricings { get; set; }
    
    public int MaterialId { get; set; }
    public int DepotId { get; set; }
}

public class UpdateMaterialPricingItemCommand
{
    public int? DepotMaterialPriceId { get; set; }
    public int UnitId { get; set; }
    public decimal? Price { get; set; }
}