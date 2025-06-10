namespace HaulageSystem.Application.Models.Depots;

public class GetDepotResponse
{
    public int DepotId { get; set; }
    public string DepotName { get; set; }
    public string Address { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public bool IsActive { get; set; }
}