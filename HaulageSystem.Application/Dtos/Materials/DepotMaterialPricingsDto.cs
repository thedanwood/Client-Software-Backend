namespace HaulageSystem.Application.Domain.Dtos.Materials;

public class DepotMaterialPricingsDto
{
    public DepotMaterialPricingsDto()
    {
        Pricings = new();
    }
    public int MaterialId { get; set; }
    public string MaterialName { get; set; }
    public List<DepotMaterialPricingDto> Pricings { get; set; }
}

public class DepotMaterialPricingInitialDataDto
{
    public List<DepotMaterialPricingsDto> DepotMaterials { get; set; }
    public List<MaterialUnitDto> AllMaterialUnits { get; set; }
}