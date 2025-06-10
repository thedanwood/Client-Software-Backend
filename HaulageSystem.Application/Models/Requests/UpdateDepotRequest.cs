using HaulageSystem.Application.Domain.Entities.Database;

namespace HaulageSystem.Application.Models.Requests;
public class UpdateDepotRequest
{
    public int DepotId { get; set; }
    public string DepotName { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public string FullAddress { get; set; }
    public bool IsActive { get; set; }
    public string UpdatedByUsername { get; set; }
}