using EnumsNET;
using HaulageSystem.Application.Domain.Dtos.Materials;
using HaulageSystem.Application.Domain.Entities.Materials;
using HaulageSystem.Application.Domain.Interfaces.Repositories;
using HaulageSystem.Application.Domain.Interfaces.Services;
using HaulageSystem.Application.Helpers;
using HaulageSystem.Domain.Enums;

namespace HaulageSystem.Application.Domain.Services;

public class MaterialsService : IMaterialsService
{
    private readonly IMaterialsRepository _materialsRepositority;

    public MaterialsService(IMaterialsRepository materialsRepository)
    {
        _materialsRepositority = materialsRepository;
    }

    public async Task<List<GetMaterialUnitResponse>> GetMaterialUnits()
    {
        return Enum.GetValues(typeof(MaterialUnits)).Cast<MaterialUnits>().Select(x => new GetMaterialUnitResponse()
        {
            UnitId = (int)x,
            UnitName = x.ToDescription()
        }).ToList();
    }
    public async Task<GetMaterialUnitResponse> GetMaterialUnit(int id)
    {
        return Enum.GetValues(typeof(MaterialUnits)).Cast<MaterialUnits>().Select(x => new GetMaterialUnitResponse()
        {
            UnitId = (int)x,
            UnitName = x.ToDescription()
        }).Where(x=> x.UnitId == id).FirstOrDefault();
    }
    
    public MaterialUnits GetMaterialUnitEnum(int id)
    {
        return Enum.GetValues(typeof(MaterialUnits)).Cast<MaterialUnits>().Where(x=> x.ToInt() == id).FirstOrDefault();
    }
}