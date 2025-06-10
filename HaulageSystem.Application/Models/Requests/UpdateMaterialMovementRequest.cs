using HaulageSystem.Application.Domain.Entities.Database;

namespace HaulageSystem.Application.Models.Requests;

public class UpdateMaterialMovementRequest
{
    public int MaterialMovementId { get; set; }
    public int? DepotMaterialPriceId { get; set; }
    public int? Quantity { get; set; }
    public int? MaterialUnitId { get; set; }
    public decimal? DefaultTotalMaterialPrice { get; set; }
    public decimal? TotalMaterialPrice { get; set; }
    public decimal? DefaultMaterialPricePerQuantityUnit { get; set; }
    public decimal? MaterialPricePerQuantityUnit { get; set; }
}