namespace HaulageSystem.Application.Models.Requests;

public class CreateMaterialPricingRequest
{
    public int MaterialId { get; set; }
    public int DepotId { get; set; }
    public int UnitId { get; set; }
    public decimal Price { get; set; }
    public bool IsActive { get; set; }
    public int ActiveState { get; set; }
    public string CreatedByUsername { get; set; }
}