using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HaulageSystem.Application.Domain.Entities.Database;

public class DepotMaterialPrices
{
    [Key] 
    public int DepotMaterialPriceId { get; set; }
    public int MaterialId { get; set; }
    public int DepotId { get; set; }
    public int MaterialUnitEnum { get; set; }
    public decimal MaterialPrice { get; set; }
    public int ActiveState { get; set; }
}