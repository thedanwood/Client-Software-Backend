using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HaulageSystem.Application.Domain.Entities.Database;

public class MaterialMovements
{
    [Key] public int MaterialMovementId { get; set; }

    //[ForeignKey("HaulageMovements")]
    public int DeliveryMovementId { get; set; }

    public int DepotMaterialPriceId { get; set; }
    public int Quantity { get; set; }
    public int MaterialUnitId { get; set; }
    public decimal DefaultTotalMaterialPrice { get; set; }
    public decimal DefaultMaterialPricePerQuantityUnit { get; set; }
    public decimal TotalMaterialPrice { get; set; }
    public decimal MaterialPricePerQuantityUnit { get; set; }
    
    //public virtual HaulageMovements HaulageMovements { get; set; }
}