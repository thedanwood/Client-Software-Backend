using System.ComponentModel.DataAnnotations.Schema;
using HaulageSystem.Application.Domain.Entities.Database;
using Microsoft.EntityFrameworkCore;

namespace HaulageSystem.Peristance.Interfaces;

public interface IHaulagePlannerDbContext
{
    DbSet<Audits> Audits { get; set; }
    DbSet<Materials> Materials { get; set; }
    DbSet<Depots> Depots { get; set; }
    DbSet<DepotMaterialPrices> DepotMaterialPrices { get; set; }
    DbSet<Quotes> Quotes { get; set; }
    DbSet<MaterialMovements> MaterialMovements { get; set; }
    DbSet<DeliveryMovements> DeliveryMovements { get; set; }
    DbSet<Companies> Companies { get; set; }
    DbSet<VehicleTypes> VehicleTypes { get; set; }

    Task<int> SaveChangesAsync(string username, CancellationToken cancellationToken = default);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
}