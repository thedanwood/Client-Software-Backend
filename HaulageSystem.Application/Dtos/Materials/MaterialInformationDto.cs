namespace HaulageSystem.Application.Dtos.Materials;

public class MaterialInformationDto
{
    public int MaterialId { get; set; }
    public string MaterialName { get; set; }
    public decimal? HighestPrice { get; set; }
    public decimal? SinglePrice { get; set; }
    public decimal? LowestPrice { get; set; }
}