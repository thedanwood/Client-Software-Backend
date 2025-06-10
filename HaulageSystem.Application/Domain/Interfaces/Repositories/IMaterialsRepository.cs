using HaulageSystem.Application.Domain.Dtos.Materials;
using HaulageSystem.Application.Domain.Entities.Materials;
using HaulageSystem.Application.Models.Requests;
using HaulageSystem.Domain.Enums;
using OneOf;
using OneOf.Types;

namespace HaulageSystem.Application.Domain.Interfaces.Repositories;

public interface IMaterialsRepository
{
    Task<List<GetMaterialResponse>> GetMaterials();
    Task<List<GetMaterialResponse>> GetMaterials(List<int> materialIds);
    Task<int> CreateMaterialAsync(string materialName);
    Task UpdateMaterialAsync(int id, string materialName);
    Task DeleteMaterialsAsync(List<int> ids);
    Task<GetMaterialResponse> GetMaterial(int id);
}