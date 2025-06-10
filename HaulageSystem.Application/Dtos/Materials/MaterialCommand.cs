namespace HaulageSystem.Application.Domain.Dtos.Materials;

public class MaterialCommand
{
    public int DepotMaterialPriceId { get; set; }
    public int Quantity { get; set; }
    public int VehicleTypeId { get; set; }
    public int DispatchDepotId { get; set; }
}