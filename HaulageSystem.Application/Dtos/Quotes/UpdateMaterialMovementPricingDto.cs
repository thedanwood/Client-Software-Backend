namespace HaulageSystem.Application.Dtos.Quotes;

public class UpdateMaterialMovementPricingDto
{

    public decimal DefaultTotalMaterialPrice { get; set; }
    public decimal TotalMaterialPrice { get; set; }
    public decimal MaterialPricePerQuantityUnit { get; set; }
    public decimal DefaultMaterialPricePerQuantityUnit { get; set; }
}