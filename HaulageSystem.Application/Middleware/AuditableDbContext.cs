using EnumsNET;
using HaulageSystem.Application.Domain.Entities.Database;
using HaulageSystem.Application.Domain.Interfaces.Services;
using HaulageSystem.Application.Helpers;
using HaulageSystem.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;

namespace HaulageSystem.Application.Middleware
{
    public class AuditEntry
    {
        public AuditEntry(EntityEntry entry)
        {
            Entry = entry;
        }
        public EntityEntry Entry { get; }
        public string ChangedByUsername { get; set; }
        public string ChangedTableName { get; set; }
        public Dictionary<string, object> KeyValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> OldValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> NewValues { get; } = new Dictionary<string, object>();
        public AuditTypes AuditType { get; set; }
        public List<string> ChangedColumns { get; } = new List<string>();
        public Audits ToAudit()
        {
            var audit = new Audits();
            audit.ChangedByUsername = ChangedByUsername;
            audit.AuditType = EnumHelpers.ToInt(AuditType);
            audit.ChangedTableName = ChangedTableName;
            audit.ChangedDateTime = DateTime.Now;
            audit.ChangedPrimaryKey = JsonConvert.SerializeObject(KeyValues);
            audit.OldValues = OldValues.Count == 0 ? null : JsonConvert.SerializeObject(OldValues);
            audit.NewValues = NewValues.Count == 0 ? null : JsonConvert.SerializeObject(NewValues);
            return audit;
        }
    }
    
}
