namespace HaulageSystem.Application.Domain.Dtos.Quotes;

public class MaterialMovementForDisplayDto
{
    public int DepotMaterialPriceId { get; set; }
    public decimal Price { get; set; }
    public string DepotName { get; set; }
    public int JourneyTime { get; set; }
}