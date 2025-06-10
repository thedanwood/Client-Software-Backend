using HaulageSystem.Domain.Interfaces;

namespace HaulageSystem.Shared.Repositories;

public class DeliveryRepository : IDeliveryRepository
{
    // private readonly HaulagePlannerDbContextWithFactory _context;
    // public DeliveryRepository(HaulagePlannerDbContextWithFactory context)
    // {
    //     _context = context ?? throw new ArgumentNullException(nameof(context));
    // }
    // public async Task<VehicleTypes> getVehicleTypeAsync(int VehicleTypeId)
    // {
    //     return await _context.VehicleTypes.FirstOrDefaultAsync(x => x.VehicleTypeId == VehicleTypeId);
    // }
}