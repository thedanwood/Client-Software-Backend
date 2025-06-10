namespace HaulageSystem.Application.Domain.Dtos.Vehicles;

public class VehicleTypeDto
{
    public VehicleTypeDto(int id, string name, int capacity)
    {
        Id = id;
        Name = name;
        VehicleCapacity = capacity;
    }
    public int Id { get; set; }
    public string Name { get; set; }
    public int VehicleCapacity { get; set; }
}