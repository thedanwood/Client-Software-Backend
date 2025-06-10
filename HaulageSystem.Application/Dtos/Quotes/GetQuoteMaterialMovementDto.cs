using HaulageSystem.Application.Domain.Dtos.Materials;
using HaulageSystem.Application.Dtos.Materials;

namespace HaulageSystem.Application.Dtos.Quotes;

public class GetQuoteMaterialMovementDto
{
    public int MaterialMovementId { get; set; }
    public MaterialDto Material { get; set; }
    public int Quantity { get; set; }
    public MaterialUnitDto MaterialUnit { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal PricePerQuantityUnit { get; set; }
    public string DepotName { get; set; }
}