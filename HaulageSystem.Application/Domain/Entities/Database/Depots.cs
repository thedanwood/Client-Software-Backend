using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HaulageSystem.Application.Domain.Entities.Database;

public class Depots
{
    [Key] 
    public int DepotId { get; set; }
    public string DepotName { get; set; }
    public string Address { get; set; }
    public decimal Longitude { get; set; }
    public decimal Latitude { get; set; }
    public bool Active { get; set; }
}