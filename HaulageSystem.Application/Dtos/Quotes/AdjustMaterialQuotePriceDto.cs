using HaulageSystem.Application.Models.Quotes;

namespace HaulageSystem.Application.Dtos.Quotes;

public class AdjustMaterialQuotePriceDto
{
    public decimal DefaultTotalMaterialPrice { get; set; }
    public decimal TotalMaterialPrice { get; set; }
    public decimal DefaultMaterialPricePerQuantityUnit { get; set; }
    public decimal MaterialPricePerQuantityUnit { get; set; }
    public string MaterialUnitName { get; set; }
    public decimal MaterialUnitId { get; set; }
    public string MaterialName { get; set; }
    public string DepotName { get; set; }
    public int Quantity { get; set; }
}