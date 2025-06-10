using HaulageSystem.Application.Domain.Dtos.Materials;
using HaulageSystem.Application.Domain.Entities.Materials;
using HaulageSystem.Domain.Enums;

namespace HaulageSystem.Application.Domain.Interfaces.Services;

public interface IMaterialsService
{
    Task<GetMaterialUnitResponse> GetMaterialUnit(int id);
    MaterialUnits GetMaterialUnitEnum(int id);
    Task<List<GetMaterialUnitResponse>> GetMaterialUnits();
}