using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HaulageSystem.Application.Domain.Entities.Database;

public class Companies
{
    [Key] public int CompanyId { get; set; }

    public string CompanyName { get; set; }
    public string? Reference { get; set; }
    public string? Email { get; set; }
    public string? Telephone { get; set; }
}