using MediatR;

namespace HaulageSystem.Application.Commands.MaterialPricing;

public class CreateMaterialPricingsCommand : IRequest
{
    public int MaterialId { get; set; }
    public int DepotId { get; set; }
    public bool IsActive { get; set; }
    public List<CreateMaterialPricingPriceCommand> Prices { get; set; }
}

public class CreateMaterialPricingPriceCommand
{
    public int UnitId { get; set; }
    public decimal? Price { get; set; }
}