namespace HaulageSystem.Application.Dtos.Quotes;

public class MaterialPriceResponseDto
{
    public decimal TotalMaterialPrice { get; set; }
    public decimal DefaultTotalMaterialPrice { get; set; }
    public decimal MaterialPricePerQuantityUnit { get; set; }
    public decimal DefaultMaterialPricePerQuantityUnit { get; set; }
}