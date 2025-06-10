using HaulageSystem.Domain.Enums;

namespace HaulageSystem.Application.Domain.Entities.Materials;

public class GetMaterialResponse
{
    public int MaterialId { get; set; }
    public string MaterialName { get; set; }
}