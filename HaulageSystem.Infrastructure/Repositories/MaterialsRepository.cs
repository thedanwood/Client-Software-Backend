using HaulageSystem.Application.Domain.Dtos.Materials;
using HaulageSystem.Application.Domain.Mappers;
using HaulageSystem.Application.Domain.Entities.Database;
using HaulageSystem.Application.Domain.Entities.Materials;
using HaulageSystem.Application.Domain.Interfaces.Repositories;
using HaulageSystem.Application.Domain.Interfaces.Services;
using HaulageSystem.Application.Exceptions;
using HaulageSystem.Application.Models.Requests;
using HaulageSystem.Domain.Enums;
using HaulageSystem.Domain.Interfaces;
using HaulageSystem.Peristance.Interfaces;
using HaulageSystem.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OneOf;
using OneOf.Types;

namespace HaulageSystem.Shared.Repositories;

public class MaterialsRepository : IMaterialsRepository
{
    private readonly IHaulagePlannerDbContext _context;
    private readonly IUserService _userService;

    public MaterialsRepository(IHaulagePlannerDbContext context, IUserService userService)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _userService = userService;
    }
    
    public async Task<List<GetMaterialResponse>> GetMaterials(List<int> materialIds)
    {
        var materials = await _context.Materials.Where(x => materialIds.Contains(x.MaterialId)).ToListAsync();
        return materials.Select(x => x.ToResponse()).ToList();
    }
    public async Task<List<GetMaterialResponse>> GetMaterials()
    {
        var materials = await _context.Materials.ToListAsync();
        return materials.Select(x => x.ToResponse()).ToList();
    }

    public async Task<int> CreateMaterialAsync(string materialName)
    {
        var material = new Materials()
        {
            MaterialName = materialName
        };
        _context.Materials.Add(material);
        await _context.SaveChangesAsync(await _userService.GetCurrentUsername());
        return material.MaterialId;
    }
    public async Task UpdateMaterialAsync(int id, string materialName)
    {
        var material = await _context.Materials.FirstOrDefaultAsync(x => x.MaterialId == id);

        material.MaterialName = materialName;
        await _context.SaveChangesAsync(await _userService.GetCurrentUsername());
    }
    public async Task DeleteMaterialsAsync(List<int> ids)
    {
        var materials = await _context.Materials.Where(x => ids.Contains(x.MaterialId)).ToListAsync();

        _context.Materials.RemoveRange(materials);
        await _context.SaveChangesAsync(await _userService.GetCurrentUsername());
    }

    public async Task<GetMaterialResponse> GetMaterial(int id)
    {
        var material = await _context.Materials.FirstOrDefaultAsync(x => x.MaterialId == id);
        if (material is null)
        {
            throw new MaterialNotFoundException(id);
        }

        return material.ToResponse();
    }
}