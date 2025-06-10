using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HaulageSystem.Application.Domain.Entities.Database;

public class Materials
{
    [Key]
    public int MaterialId { get; set; }
    public string MaterialName { get; set; }

    public bool Active { get; set; }
}