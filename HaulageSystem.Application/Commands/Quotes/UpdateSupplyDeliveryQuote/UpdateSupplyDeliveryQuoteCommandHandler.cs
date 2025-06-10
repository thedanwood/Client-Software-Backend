using System.Data;
using HaulageSystem.Application.Core.Commands.Quotes;
using HaulageSystem.Application.Domain.Entities.Quotes;
using HaulageSystem.Application.Domain.Interfaces.Repositories;
using HaulageSystem.Application.Domain.Interfaces.Services;
using HaulageSystem.Application.Dtos.Quotes;
using HaulageSystem.Application.Models.Quotes;
using HaulageSystem.Application.Models.Requests;
using HaulageSystem.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore.Query;

namespace HaulageSystem.Application.Commands.Quotes;

public class UpdateSupplyDeliveryQuoteCommandHandler : IRequestHandler<UpdateSupplyDeliveryQuoteCommand>
{
    private readonly IQuotesRepository _quotesRepository;
    private readonly IQuotesService _quotesService;
    private readonly IUserService _userService;

    public UpdateSupplyDeliveryQuoteCommandHandler(IQuotesRepository quotesRepository,
        IUserService userService, IQuotesService quotesService)
    {
        _quotesRepository = quotesRepository;
        _userService = userService;
        _quotesService = quotesService;
    }

    public async Task Handle(UpdateSupplyDeliveryQuoteCommand command,
        CancellationToken cancellationToken)
    {
        var quote = await _quotesRepository.GetQuote(command.QuoteId);
        await _quotesRepository.UpdateQuote(GetUpdateRecordRequest(command, quote));
        
        var existingDeliveryMovements = await _quotesRepository.GetDeliveryMovementsAsync(quote.QuoteId);
        var existingMaterialMovements = new List<GetMaterialMovementResponse>();
        foreach (var existingDeliveryMovement in existingDeliveryMovements)
        {
            existingMaterialMovements.AddRange(await _quotesRepository.GetMaterialMovementsAsync(existingDeliveryMovement.DeliveryMovementId));
        }
        var existingMaterialMovementIds = existingMaterialMovements.Select(x => x.MaterialMovementId).ToList();
        
        var addedMaterialMovements = command.Movements.Where(x => x.MaterialMovementId == null).ToList();
        var removedMaterialMovements = existingMaterialMovements.Where(existingMovement =>
            command.Movements.All(newMovement =>
                newMovement.MaterialMovementId != existingMovement.MaterialMovementId)).ToList();
        var updatedMovements = command.Movements.Where(updatedMovement =>
            updatedMovement.MaterialMovementId != null &&
            existingMaterialMovementIds.Contains(updatedMovement.MaterialMovementId.Value)).ToList();
        
        if (addedMaterialMovements.Any())
        {
            foreach (var addedMovement in addedMaterialMovements)
            {
                var createDeliveryMovementRequest = GetCreateDeliveryMovementRequest(addedMovement, quote.QuoteId);
                var deliveryMovementId = await _quotesRepository.CreateDeliveryMovement(createDeliveryMovementRequest);
                var createMaterialMovementRequest = GetCreateMaterialMovementRequest(addedMovement, deliveryMovementId);
                await _quotesRepository.CreateMaterialMovement(createMaterialMovementRequest);
            }
        }
        
        if (removedMaterialMovements.Any())
        {
            foreach (var removedMovement in removedMaterialMovements)
            {
                await _quotesRepository.DeleteDeliveryMovement(removedMovement.DeliveryMovementId);
                await _quotesRepository.DeleteMaterialMovement(removedMovement.MaterialMovementId);
            }
        }
        
        foreach (var updatedMovement in updatedMovements)
        {
            var deliveryMovement =
                existingMaterialMovements.First(
                    x => x.MaterialMovementId == updatedMovement.MaterialMovementId);
            await _quotesRepository.UpdateDeliveryMovement(GetUpdateDeliveryMovementRequest(updatedMovement, deliveryMovement.DeliveryMovementId));
            await _quotesRepository.UpdateMaterialMovement(GetUpdateMaterialMovementRequest(updatedMovement));
        }
    }

    private static UpdateDeliveryMovementRequest GetUpdateDeliveryMovementRequest(UpdateSupplyDeliveryMovementCommand movement, int deliveryMovementId)
    {
        var updateDeliveryRequest = new UpdateDeliveryMovementRequest()
        {
            DeliveryMovementId = deliveryMovementId,
            VehicleTypeId = movement.VehicleTypeId,
            NumberOfLoads = movement.NumberOfLoads,
            DefaultOnewayJourneyTime = movement.DeliveryPricing.DefaultOnewayJourneyTime,
            OnewayJourneyTime = movement.DeliveryPricing.OnewayJourneyTime,
            DefaultTotalDeliveryPrice = movement.DeliveryPricing.DefaultTotalDeliveryPrice,
            TotalDeliveryPrice = movement.DeliveryPricing.TotalDeliveryPrice,
            DefaultDeliveryPricePerMinute = movement.DeliveryPricing.DefaultDeliveryPricePerMinute,
            DeliveryPricePerMinute = movement.DeliveryPricing.DeliveryPricePerMinute,
            HasTrafficEnabled = movement.HasTrafficEnabled
        };
        return updateDeliveryRequest;
    }
    
    private static UpdateMaterialMovementRequest GetUpdateMaterialMovementRequest(UpdateSupplyDeliveryMovementCommand movement)
    {
        var updateRequest = new UpdateMaterialMovementRequest()
        {
            MaterialMovementId = movement.MaterialMovementId.Value,
            DepotMaterialPriceId = movement.DepotMaterialPriceId,
            Quantity = movement.Quantity,
            MaterialUnitId = movement.MaterialUnitId,
            DefaultTotalMaterialPrice = movement.MaterialPricing.DefaultTotalMaterialPrice,
            TotalMaterialPrice = movement.MaterialPricing.TotalMaterialPrice,
            DefaultMaterialPricePerQuantityUnit = movement.MaterialPricing.DefaultMaterialPricePerQuantityUnit,
            MaterialPricePerQuantityUnit = movement.MaterialPricing.MaterialPricePerQuantityUnit
        };
        return updateRequest;
    }
    
    private static CreateDeliveryMovementRequest GetCreateDeliveryMovementRequest(UpdateSupplyDeliveryMovementCommand movement, int quoteId)
    {
        var updateDeliveryRequest = new CreateDeliveryMovementRequest()
        {
            QuoteId = quoteId,
            VehicleTypeId = movement.VehicleTypeId,
            NumberOfLoads = movement.NumberOfLoads,
            DefaultOnewayJourneyTime = movement.DeliveryPricing.DefaultOnewayJourneyTime,
            OnewayJourneyTime = movement.DeliveryPricing.OnewayJourneyTime,
            DefaultTotalDeliveryPrice = movement.DeliveryPricing.DefaultTotalDeliveryPrice,
            TotalDeliveryPrice = movement.DeliveryPricing.TotalDeliveryPrice,
            DefaultDeliveryPricePerDeliveryTimeUnit = movement.DeliveryPricing.DefaultDeliveryPricePerMinute,
            DeliveryPricePerDeliveryTimeUnit = movement.DeliveryPricing.DeliveryPricePerMinute,
            HasTrafficEnabled = movement.HasTrafficEnabled
        };
        return updateDeliveryRequest;
    }
    
    private static CreateMaterialMovementRequest GetCreateMaterialMovementRequest(UpdateSupplyDeliveryMovementCommand movement, int deliveryMovementId)
    {
        var updateDeliveryRequest = new CreateMaterialMovementRequest()
        {
            DeliveryMovementId = deliveryMovementId,
            MaterialUnitId = movement.MaterialUnitId,
            Quantity = movement.Quantity,
            DepotMaterialPriceId = movement.DepotMaterialPriceId,
            TotalMaterialPrice = movement.MaterialPricing.TotalMaterialPrice,
            DefaultTotalMaterialPrice = movement.MaterialPricing.DefaultTotalMaterialPrice,
            MaterialPricePerQuantityUnit = movement.MaterialPricing.MaterialPricePerQuantityUnit,
            DefaultMaterialPricePerQuantityUnit = movement.MaterialPricing.DefaultMaterialPricePerQuantityUnit,
        };
        return updateDeliveryRequest;
    }

    private static UpdateRecordRequest GetUpdateRecordRequest(UpdateSupplyDeliveryQuoteCommand command,
        GetQuoteResponse quote)
    {
        var updateQuoteRequest = new UpdateRecordRequest()
        {
            QuoteId = command.QuoteId,
            RecordType = quote.RecordType,
            RecordVariation = quote.RecordVariation,
            ActiveStateEnumValue = quote.ActiveStateEnumValue,
            DeliveryDate = command.DeliveryDateTime,
            Comments = command.Comments,
            CompanyId = command.CompanyId,
            DeliveryLocationFullAddress = command.DeliveryLocation.FullAddress,
            DeliveryLocationLatitude = command.DeliveryLocation.AddressPoint.Latitude,
            DeliveryLocationLongitude = command.DeliveryLocation.AddressPoint.Longitude,
            CustomerName = quote.CustomerName,
            CreatedByUsername = quote.CreatedByUsername,
            CreatedDateTime = quote.CreatedDateTime
        };
        return updateQuoteRequest;
    }
}