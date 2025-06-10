using System.ComponentModel;

namespace HaulageSystem.Domain.Enums;

public enum MaterialUnits
{
    [Description("Tonne")]
    Tonnes=1,
    [Description("Load")]
    Loads = 2,
}