using HaulageSystem.Application.Domain.Dtos.Companies;
using HaulageSystem.Application.Domain.Dtos.Materials;
using HaulageSystem.Application.Domain.Dtos.Vehicles;
using HaulageSystem.Application.Dtos.Materials;
using HaulageSystem.Application.Models.Quotes;

namespace HaulageSystem.Application.Dtos.Quotes;

public class UpdateQuoteSupplyDeliveryInitialDataDto
{
    public DateTime DeliveryDate { get; set; }
    public string Comments { get; set; }
    public CompanyDto CompanyInfo { get; set; }
    public string CustomerName { get; set; }
    public List<VehicleTypeDto> VehicleTypes { get; set; }
    public AddressDto DeliveryLocation { get; set; }
    public int NumberOfLoads { get; set; }
    public List<MaterialDto> Materials { get; set; }
    public List<MaterialUnitDto> MaterialUnits { get; set; }
    public bool DefaultHasTrafficEnabled { get; set; }
    public List<UpdateQuoteSupplyDeliveryMovementDto> DeliveryMovements { get; set; }
}
