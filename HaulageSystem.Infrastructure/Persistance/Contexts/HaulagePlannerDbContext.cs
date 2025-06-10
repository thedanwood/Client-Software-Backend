using HaulageSystem.Application.Domain.Entities.Database;
using HaulageSystem.Application.Domain.Interfaces.Services;
using HaulageSystem.Application.Middleware;
using HaulageSystem.Application.Models.Identity;
using HaulageSystem.Domain.Enums;
using HaulageSystem.Peristance.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace HaulageSystem.Persistance.Contexts;

public class HaulagePlannerDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>, IHaulagePlannerDbContext
{
    public HaulagePlannerDbContext(
        DbContextOptions<HaulagePlannerDbContext> options)
        : base(options)
    {
    }
    public DbSet<Audits> Audits { get; set; }
    public DbSet<Materials> Materials { get; set; }
    public DbSet<Depots> Depots { get; set; }
    public DbSet<DepotMaterialPrices> DepotMaterialPrices { get; set; }
    public DbSet<Quotes> Quotes { get; set; }
    public DbSet<MaterialMovements> MaterialMovements { get; set; }
    public DbSet<DeliveryMovements> DeliveryMovements { get; set; }
    public DbSet<Companies> Companies { get; set; }
    public DbSet<VehicleTypes> VehicleTypes { get; set; }
    
    public DbSet<Audits> AuditLogs { get; set; }

    public async Task<int> SaveChangesAsync(string username, CancellationToken cancellationToken = default)
    {
        OnBeforeSaveChanges(username);
        var result = await base.SaveChangesAsync(cancellationToken);
        return result;
    }

    private void OnBeforeSaveChanges(string username)
    {
        ChangeTracker.DetectChanges();
        var auditEntries = new List<AuditEntry>();
        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.Entity is Audits || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                continue;

            var auditEntry = new AuditEntry(entry)
            {
                ChangedTableName = entry.Entity.GetType().Name,
                ChangedByUsername = username
            };

            auditEntries.Add(auditEntry);

            foreach (var property in entry.Properties)
            {
                string propertyName = property.Metadata.Name;
                if (property.Metadata.IsPrimaryKey())
                {
                    auditEntry.KeyValues[propertyName] = property.CurrentValue;
                    continue;
                }

                switch (entry.State)
                {
                    case EntityState.Added:
                        auditEntry.AuditType = AuditTypes.Create;
                        auditEntry.NewValues[propertyName] = property.CurrentValue;
                        break;
                    case EntityState.Deleted:
                        auditEntry.AuditType = AuditTypes.Delete;
                        auditEntry.OldValues[propertyName] = property.OriginalValue;
                        break;
                    case EntityState.Modified:
                        if (property.IsModified)
                        {
                            auditEntry.ChangedColumns.Add(propertyName);
                            auditEntry.AuditType = AuditTypes.Update;
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                        }
                        break;
                }
            }
        }

        foreach (var auditEntry in auditEntries)
        {
            AuditLogs.Add(auditEntry.ToAudit());
        }
    }
}