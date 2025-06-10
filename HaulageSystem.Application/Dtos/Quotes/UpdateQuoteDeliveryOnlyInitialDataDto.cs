using HaulageSystem.Application.Domain.Dtos.Companies;
using HaulageSystem.Application.Domain.Dtos.Materials;
using HaulageSystem.Application.Domain.Dtos.Vehicles;
using HaulageSystem.Application.Models.Quotes;
using HaulageSystem.Domain.Enums;

namespace HaulageSystem.Application.Dtos.Quotes;

public class UpdateQuoteDeliveryOnlyInitialDataDto
{
    public DateTime DeliveryDate { get; set; }
    public string Comments { get; set; }
    public CompanyDto CompanyInfo { get; set; }
    public string CustomerName { get; set; }
    public List<VehicleTypeDto> VehicleTypes { get; set; }
    public AddressDto DeliveryLocation { get; set; }
    public int NumberOfLoads { get; set; }
    public bool DefaultHasTrafficEnabled { get; set; }

    public List<UpdateQuoteDeliveryOnlyDeliveryMovementDto> DeliveryMovements { get; set; }
}
