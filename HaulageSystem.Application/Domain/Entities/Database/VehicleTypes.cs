using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HaulageSystem.Application.Domain.Entities.Database;

public class VehicleTypes
{
    [Key] public int VehicleTypeId { get; set; }
    public string VehicleTypeName { get; set; }
    public int VehicleTypeCapacity { get; set; }
}