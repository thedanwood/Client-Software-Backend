using HaulageSystem.Application.Domain.Entities.Vehicles;

namespace HaulageSystem.Application.Domain.Interfaces.Repositories;
public interface IVehiclesRepository
{
    Task<int> GetVehicleCapacity(int Id);
    Task<List<GetVehicleTypeResponse>> GetAllVehicleTypes();
    Task<GetVehicleTypeResponse> GetVehicleType(int id);
}