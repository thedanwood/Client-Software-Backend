using HaulageSystem.Application.Dtos.Profile;
using HaulageSystem.Application.Dtos.Quotes;
using HaulageSystem.Domain.Enums;

namespace HaulageSystem.Application.Dtos.Settings;

public class SettingsInitialDataDto
{
    public DeliveryUnitDto DeliveryUnit { get; set; }
    public List<ApplyMaximumCapacitySettingDto> ApplyMaximumCapacitySettings { get; set; }

    //used to force material units into generated file
    //TODO find another way to do this?
    public MaterialUnits _;
}