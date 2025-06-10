using HaulageSystem.Domain.Enums;

namespace HaulageSystem.Application.Domain.Entities.Materials;

public class GetMaterialUnitResponse
{
    public int UnitId { get; set; }
    public string UnitName { get; set; }
}