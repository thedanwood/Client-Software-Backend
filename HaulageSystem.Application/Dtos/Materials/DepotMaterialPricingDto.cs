namespace HaulageSystem.Application.Domain.Dtos.Materials;

public class DepotMaterialPricingDto
{
    public int DepotMaterialPriceId { get; set; }
    public int UnitId { get; set; }
    public string UnitName { get; set; }
    public decimal Price { get; set; }
}