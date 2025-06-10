using HaulageSystem.Application.Domain.Dtos.Materials;
using HaulageSystem.Application.Domain.Dtos.Vehicles;
using HaulageSystem.Application.Dtos.Materials;

namespace HaulageSystem.Application.Dtos.Quotes;

public class MaterialPricingInitialDataDto
{
    public string DepotName { get; set; }
    public List<MaterialDto> Materials { get; set; }
}