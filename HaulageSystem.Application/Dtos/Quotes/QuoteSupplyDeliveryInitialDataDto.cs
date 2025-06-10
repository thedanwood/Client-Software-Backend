using HaulageSystem.Application.Domain.Dtos.Materials;
using HaulageSystem.Application.Domain.Dtos.Vehicles;
using HaulageSystem.Application.Dtos.Materials;
using HaulageSystem.Domain.Enums;

namespace HaulageSystem.Application.Dtos.Quotes;

public class QuoteSupplyDeliveryInitialDataDto
{
    public List<VehicleTypeDto> VehicleTypes { get; set; }
    public List<MaterialDto> Materials { get; set; }
    public List<SelectedMaterialDto> SelectedMaterials { get; set; }
    public List<MaterialUnitDto> MaterialUnits { get; set; }
    //added for access to enum in fe
    public RoutingParameters RoutingParameters { get; set; }
    public bool DefaultHasTrafficEnabled { get; set; }
}