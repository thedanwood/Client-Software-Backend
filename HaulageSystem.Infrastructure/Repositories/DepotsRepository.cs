using Azure.Core;
using HaulageSystem.Application.Domain.Entities.Database;
using HaulageSystem.Application.Domain.Interfaces.Repositories;
using HaulageSystem.Application.Domain.Interfaces.Services;
using HaulageSystem.Application.Domain.Mappers;
using HaulageSystem.Application.Exceptions;
using HaulageSystem.Application.Models.Depots;
using HaulageSystem.Application.Models.Requests;
using HaulageSystem.Domain.Interfaces;
using HaulageSystem.Peristance.Interfaces;
using HaulageSystem.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using OneOf;
using OneOf.Types;

namespace HaulageSystem.Shared.Repositories;

public class DepotsRepository : IDepotsRepository
{
    private readonly IHaulagePlannerDbContext _context;
    private readonly IUserService _userService;

    public DepotsRepository(IHaulagePlannerDbContext context, IUserService userService)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _userService = userService;
    }

    public async Task<int> CreateDepot(string depotName, decimal latitude, decimal longitude, string fullAddress, bool isActive)
    {
        var depot = new Depots()
        {
            DepotName = depotName,
            Latitude = latitude,
            Longitude = longitude,
            Address = fullAddress,
            Active = isActive
        };
        _context.Depots.Add(depot);
        await _context.SaveChangesAsync(await _userService.GetCurrentUsername());
        return depot.DepotId;
    }

    public async Task DeleteDepots(List<int> depotIds)
    {
        var depots = await _context.Depots.Where(x => depotIds.Contains(x.DepotId)).ToListAsync();

        _context.Depots.RemoveRange(depots);
        await _context.SaveChangesAsync(await _userService.GetCurrentUsername());
    }

    public async Task UpdateDepot(UpdateDepotRequest request)
    {
        var depot = await _context.Depots.FirstOrDefaultAsync(x => x.DepotId == request.DepotId);
        if (depot is null)
        {
            throw new DepotNotFoundException(request.DepotId, null);
        }

        depot.DepotName = request.DepotName;
        depot.Latitude = request.Latitude;
        depot.Longitude = request.Longitude;
        depot.Active = request.IsActive;
        depot.Address = request.FullAddress;
        await _context.SaveChangesAsync(await _userService.GetCurrentUsername());
    }
    
    public async Task<List<GetDepotResponse>> GetDepot(List<int> ids)
    {
        var depot = await _context.Depots.Where(x => ids.Contains(x.DepotId)).ToListAsync();
        return depot.Select(x=>x.ToResponse()).ToList();
    }
    
    public async Task<GetDepotResponse> GetDepot(int Id)
    {
        var result = await _context.Depots.FirstOrDefaultAsync(x => x.DepotId == Id);
        return result.ToResponse();
    }
    public async Task<List<GetDepotResponse>> GetDepots(List<int> depotIds, bool includeInactive = false)
    {
        var depots = await _context.Depots.Where(x => depotIds.Contains(x.DepotId)).ToListAsync();
        if (!includeInactive)
        {
            depots = depots.Where(x => x.Active).ToList();
        }
        return depots.Select(x => x.ToResponse()).ToList();
    }
    public async Task<List<GetDepotResponse>> GetAllDepots(bool includeInactive = false)
    {
        var depots = await _context.Depots.Where(x => x.Active == !includeInactive).ToListAsync();
        return depots.Select(x => x.ToResponse()).ToList();
    }
}