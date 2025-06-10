namespace HaulageSystem.Application.Domain.Entities.Profile;

public class GetApplyMaximumCapacitySettingsResponse
{

    public int MaterialUnitId { get; set; }
    public List<int> ApplyMaximumCapacityFromVehicleTypes { get; set; }
}