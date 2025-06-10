using HaulageSystem.Application.Domain.Entities.Quotes;
using HaulageSystem.Application.Domain.Interfaces.Repositories;
using HaulageSystem.Application.Domain.Interfaces.Services;
using HaulageSystem.Application.Domain.Mappers;
using HaulageSystem.Application.Dtos.Quotes;
using HaulageSystem.Application.Helpers;
using HaulageSystem.Application.Models.Requests;
using HaulageSystem.Domain.Enums;
using HaulageSystem.Peristance.Interfaces;
using Microsoft.EntityFrameworkCore;
using OneOf;
using OneOf.Types;
using Serilog;

namespace HaulageSystem.Shared.Repositories;

public class QuotesRepository : IQuotesRepository
{
    private readonly IVehiclesRepository _vehiclesRepository;
    private readonly IHaulagePlannerDbContext _context;
    private readonly IUserService _userService;
    public QuotesRepository(IHaulagePlannerDbContext context, IVehiclesRepository vehiclesRepository, IUserService userService)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _vehiclesRepository = vehiclesRepository;
        _userService = userService;
    }
    public async Task<List<GetQuoteResponse>> GetTestQuotes()
    {
        var quotes =  await _context.Quotes
            .Where(x => x.CreatedDateTime > DateTime.Now.AddDays(-90))
            .Select(x => x.ToResponse()).ToListAsync();
        var quoteIds = quotes.Select(x => x.QuoteId).ToList();

        var companies = _context.Companies.Where(x => quoteIds.Contains(x.CompanyId)).ToList();
        var deliveryMovements = await _context.DeliveryMovements.Where(x => quoteIds.Contains(x.QuoteId)).ToListAsync();
        var deliveryMovementIds = deliveryMovements.Select(x => x.DeliveryMovementId).ToList();

        var vehicleTypes = await _vehiclesRepository.GetAllVehicleTypes();
        var materialMovements = await _context.MaterialMovements
            .Where(x => deliveryMovementIds.Contains(x.MaterialMovementId)).ToListAsync();
        var MaterialMovementIds = materialMovements.Select(x => x.DepotMaterialPriceId).ToList();

        var pricings = await _context.DepotMaterialPrices
            .Where(x => MaterialMovementIds.Contains(x.DepotMaterialPriceId)).ToListAsync();
        var materialIds = pricings.Select(x => x.DepotMaterialPriceId).ToList();
        var depotIds = pricings.Select(x => x.DepotId).ToList();

        var materials = await _context.Materials.Where(x => materialIds.Contains(x.MaterialId)).ToListAsync();
        var depots = await _context.Depots.Where(x => depotIds.Contains(x.DepotId)).ToListAsync();
        
        return quotes;
    }
    public async Task<int> CreateRecordAsync(CreateRecordRequest request)
    {
        var record = request.ToEntity();
        _context.Quotes.Add(record);
        await _context.SaveChangesAsync(await _userService.GetCurrentUsername());
        return record.QuoteId;
    }
    
    public async Task DeleteDeliveryMovement(int deliveryMovementId)
    {
        var deliveryMovement = await _context.DeliveryMovements.FirstOrDefaultAsync(x => x.DeliveryMovementId == deliveryMovementId);
        _context.DeliveryMovements.Remove(deliveryMovement);
        await _context.SaveChangesAsync(await _userService.GetCurrentUsername());
    }
    public async Task DeleteMaterialMovements(List<int> MaterialMovementIds)
    {
        var materialMovements = await _context.MaterialMovements.Where(x => MaterialMovementIds.Contains(x.MaterialMovementId)).ToListAsync();
        _context.MaterialMovements.RemoveRange(materialMovements);
        await _context.SaveChangesAsync(await _userService.GetCurrentUsername());
    }
    public async Task DeleteMaterialMovement(int MaterialMovementId)
    {
        var materialMovement = await _context.MaterialMovements.Where(x => x.MaterialMovementId ==  MaterialMovementId).FirstOrDefaultAsync();
        _context.MaterialMovements.Remove(materialMovement);
        await _context.SaveChangesAsync(await _userService.GetCurrentUsername());
    }

    public async Task<int> CreateDeliveryMovement(CreateDeliveryMovementRequest request)
    {
        var deliveryMovement = request.ToEntity();
        _context.DeliveryMovements.Add(deliveryMovement);
        await _context.SaveChangesAsync(await _userService.GetCurrentUsername());
        return deliveryMovement.DeliveryMovementId;
    }

    public async Task<int> CreateMaterialMovement(CreateMaterialMovementRequest request)
    {
        var materialMovement = request.ToEntity();
        _context.MaterialMovements.Add(materialMovement);
        await _context.SaveChangesAsync(await _userService.GetCurrentUsername());
        return materialMovement.MaterialMovementId;
    }
    
    public async Task UpdateQuoteActiveState(UpdateRecordActiveStateRequest activeStateRequest)
    {
        var quote = _context.Quotes.FirstOrDefault(x => x.QuoteId == activeStateRequest.QuoteId);
        quote.ActiveStateEnumValue = activeStateRequest.ActiveState;
        await _context.SaveChangesAsync(await _userService.GetCurrentUsername());
    }
    
    public async Task UpdateQuote(UpdateRecordRequest request)
    {
        var quote = _context.Quotes.FirstOrDefault(x => x.QuoteId == request.QuoteId);
        quote.RecordType = quote.RecordType;
        quote.ActiveStateEnumValue = request.ActiveStateEnumValue;
        quote.DeliveryDate = request.DeliveryDate;
        quote.Comments = request.Comments;
        quote.CompanyId = request.CompanyId;
        quote.DeliveryLocationFullAddress = request.DeliveryLocationFullAddress;
        quote.DeliveryLocationLatitude = request.DeliveryLocationLatitude;
        quote.DeliveryLocationLongitude = request.DeliveryLocationLongitude;
        quote.CustomerName = request.CustomerName;
        quote.CreatedByUsername = request.CreatedByUsername;
        quote.CreatedDateTime = request.CreatedDateTime;
        await _context.SaveChangesAsync(await _userService.GetCurrentUsername());
    }
    
    public async Task UpdateDeliveryMovement(UpdateDeliveryMovementRequest request)
    {
        var movement = _context.DeliveryMovements.FirstOrDefault(x => x.DeliveryMovementId == request.DeliveryMovementId);
        movement.HasTrafficEnabled = request.HasTrafficEnabled ?? movement.HasTrafficEnabled;
        movement.StartLocationFullAddress = request.StartLocationFullAddress ?? movement.StartLocationFullAddress;
        movement.StartLocationLatitude = request.StartLocationLatitude ?? movement.StartLocationLatitude;
        movement.StartLocationLongitude = request.StartLocationLongitude ?? movement.StartLocationLongitude;
        movement.NumberOfLoads = request.NumberOfLoads ?? movement.NumberOfLoads;
        movement.TotalDeliveryPrice = request.TotalDeliveryPrice ?? movement.TotalDeliveryPrice;
        movement.DefaultTotalDeliveryPrice = request.DefaultTotalDeliveryPrice ?? movement.DefaultTotalDeliveryPrice;
        movement.OnewayJourneyTime = request.OnewayJourneyTime ?? movement.OnewayJourneyTime;
        movement.DefaultOnewayJourneyTime = request.DefaultOnewayJourneyTime ?? movement.DefaultOnewayJourneyTime;
        movement.DeliveryPricePerTimeUnit = request.DeliveryPricePerMinute ?? movement.DeliveryPricePerTimeUnit;
        movement.DefaultDeliveryPricePerTimeUnit = request.DefaultDeliveryPricePerMinute ?? movement.DefaultDeliveryPricePerTimeUnit;
        await _context.SaveChangesAsync(await _userService.GetCurrentUsername());
    }
    public async Task UpdateMaterialMovement(UpdateMaterialMovementRequest request)
    {
        var movement = _context.MaterialMovements.FirstOrDefault(x => x.MaterialMovementId == request.MaterialMovementId);
        movement.DepotMaterialPriceId = request?.DepotMaterialPriceId ?? movement.DepotMaterialPriceId;
        movement.Quantity = request?.Quantity ?? movement.Quantity;
        movement.MaterialUnitId = request?.MaterialUnitId ?? movement.MaterialUnitId;
        movement.DefaultTotalMaterialPrice = request?.DefaultTotalMaterialPrice ?? movement.DefaultTotalMaterialPrice;
        movement.TotalMaterialPrice = request?.TotalMaterialPrice ?? movement.TotalMaterialPrice;
        movement.DefaultMaterialPricePerQuantityUnit = request?.DefaultMaterialPricePerQuantityUnit ?? movement.DefaultMaterialPricePerQuantityUnit;
        movement.MaterialPricePerQuantityUnit = request?.MaterialPricePerQuantityUnit ?? movement.MaterialPricePerQuantityUnit;
        await _context.SaveChangesAsync(await _userService.GetCurrentUsername());
    }

    public async Task<List<GetQuoteResponse>> GetQuotes(int fromDaysAgo)
    {
        var negativeDaysAgo = fromDaysAgo * -1;
        var fromDate = DateTime.Now.AddDays(negativeDaysAgo);
        return await _context.Quotes
            .Where(x => x.CreatedDateTime > fromDate && EnumHelpers.ActiveQuoteStates.Contains(x.ActiveStateEnumValue))
            .OrderByDescending(y => y.QuoteId)
            .Select(x => x.ToResponse()).ToListAsync();
    }

    public async Task<GetQuoteResponse> GetQuote(int quoteId)
    {
        var quote = await _context.Quotes.FirstOrDefaultAsync(x => x.QuoteId == quoteId);
        return quote.ToResponse();
    }
    public async Task SetActiveState(int quoteId, int activeState)
    {
        var record = await
            _context.Quotes.FirstOrDefaultAsync(x => x.QuoteId == quoteId);
        if (record is not null)
        {
            record.ActiveStateEnumValue = activeState;
            await _context.SaveChangesAsync(await _userService.GetCurrentUsername());
        }
    }
    public async Task<List<GetDeliveryMovementResponse>> GetDeliveryMovementsAsync(List<int> quoteIds)
    {
        return await _context.DeliveryMovements.Where(x => quoteIds.Contains(x.QuoteId))
            .Select(x => x.ToResponse()).ToListAsync();
    }
    
    public async Task<GetDeliveryMovementResponse> GetDeliveryMovementAsync(int deliveryMovementId)
    {
        return (await _context.DeliveryMovements.FirstOrDefaultAsync(x => x.DeliveryMovementId == deliveryMovementId))
            .ToResponse();
    }
    public async Task<List<GetDeliveryMovementResponse>> GetDeliveryMovementsAsync(int quoteId)
    {
        return await _context.DeliveryMovements.Where(x => x.QuoteId == quoteId)
            .Select(x => x.ToResponse()).ToListAsync();
    }
    public async Task<List<GetMaterialMovementResponse>> GetMaterialMovementsAsync(List<int> deliveryMovementIds)
    {
        return await _context.MaterialMovements.Where(x => deliveryMovementIds.Contains(x.DeliveryMovementId))
            .Select(x => x.ToResponse()).ToListAsync();
    }
    public async Task<List<GetMaterialMovementResponse>> GetMaterialMovementsAsync(int deliveryMovementId)
    {
        return await _context.MaterialMovements.Where(x => x.DeliveryMovementId == deliveryMovementId)
            .Select(x => x.ToResponse()).ToListAsync();
    }
    public async Task<GetMaterialMovementResponse> GetMaterialMovementAsync(int deliveryMovementId)
    {
        return await _context.MaterialMovements.Where(x => x.DeliveryMovementId == deliveryMovementId)
            .Select(x => x.ToResponse()).FirstOrDefaultAsync();
    }
}