namespace HaulageSystem.Application.Dtos.Profile;

public class ApplyMaximumCapacitySettingDto
{
    public int MaterialUnitId { get; set; }
    public List<int> ApplyMaximumCapacityFromVehicleTypes { get; set; }
}