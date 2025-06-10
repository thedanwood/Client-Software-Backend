using HaulageSystem.Application.Models.Quotes;

namespace HaulageSystem.Application.Dtos.Depots;

public class DepotsInformationDto
{
    public int DepotId { get; set; }
    public string DepotName { get; set; }
    public AddressDto DepotAddress { get; set; }
    public int NumberOfSuppliedMaterials { get; set; }
}