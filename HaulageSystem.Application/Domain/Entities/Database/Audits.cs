using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HaulageSystem.Application.Domain.Entities.Database;

public class Audits
{
    [Key] public int AuditId { get; set; }

    public string ChangedByUsername { get; set; }
    public DateTime ChangedDateTime{ get; set; }
    public int AuditType { get; set; }
    public string ChangedTableName { get; set; }
    public string ChangedPrimaryKey { get; set; }
    public string? OldValues { get; set; }
    public string? NewValues { get; set; }
}