namespace HaulageSystem.Application.Models.Requests;

public class UpdateMaterialPricingRequest
{
    public int DepotMaterialPriceId { get; set; }
    public int MaterialId { get; set; }
    public int DepotId { get; set; }
    public int Unit { get; set; }
    public decimal Price { get; set; }
    public bool IsActive { get; set; }
    public int ActiveState { get; set; }
}