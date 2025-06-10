using HaulageSystem.Domain.Enums;

namespace HaulageSystem.Application.Domain.Entities.Materials;

public class GetDepotMaterialPriceResponse
{
    public int DepotMaterialPriceId { get; set; }
    public int MaterialId { get; set; }
    public int DepotId { get; set; }
    public int MaterialUnitId { get; set; }
    public decimal MaterialPricePerQuantityUnit { get; set; }
    public int ActiveState { get; set; }
    public bool IsActive { get; set; }
}