using HaulageSystem.Application.Domain.Entities.Vehicles;
using HaulageSystem.Application.Domain.Interfaces.Repositories;
using HaulageSystem.Application.Domain.Mappers;
using HaulageSystem.Peristance.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HaulageSystem.Shared.Repositories;

public class VehiclesRepository : IVehiclesRepository
{
    private readonly IHaulagePlannerDbContext _context;

    public VehiclesRepository(IHaulagePlannerDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<int> GetVehicleCapacity(int Id)
    {
        var vehicle = await _context.VehicleTypes.FirstOrDefaultAsync(x => x.VehicleTypeId == Id);
        return vehicle.VehicleTypeCapacity;
    }
    public async Task<List<GetVehicleTypeResponse>> GetAllVehicleTypes()
    {
        var vehicles = await _context.VehicleTypes.ToListAsync();
        return vehicles.Select(x => x.ToResponse()).ToList();
    }
    public async Task<GetVehicleTypeResponse> GetVehicleType(int id)
    {
        var vehicles = await _context.VehicleTypes.ToListAsync();
        return vehicles.Where(x=>x.VehicleTypeId == id).Select(x => x.ToResponse()).FirstOrDefault();
    }
}