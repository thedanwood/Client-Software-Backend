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

public class UpdateDeliveryOnlyQuoteCommandHandler : IRequestHandler<UpdateDeliveryOnlyQuoteCommand>
{
    private readonly IQuotesRepository _quotesRepository;
    private readonly IQuotesService _quotesService;
    private readonly IUserService _userService;

    public UpdateDeliveryOnlyQuoteCommandHandler(IQuotesRepository quotesRepository,
        IUserService userService, IQuotesService quotesService)
    {
        _quotesRepository = quotesRepository;
        _userService = userService;
        _quotesService = quotesService;
    }

    public async Task Handle(UpdateDeliveryOnlyQuoteCommand command,
        CancellationToken cancellationToken)
    {
        var quote = await _quotesRepository.GetQuote(command.QuoteId);
        
        await _quotesRepository.UpdateQuote(GetUpdateRecordRequest(command, quote));
        
        var existingDeliveryMovements = await _quotesRepository.GetDeliveryMovementsAsync(quote.QuoteId);
        var addedDeliveryMovements = command.DeliveryMovements.Where(x => x.DeliveryMovementId == null).ToList();
        var removedDeliveryMovements = existingDeliveryMovements.Where(existingMovement =>
            command.DeliveryMovements.All(newMovement =>
                newMovement.DeliveryMovementId != existingMovement.DeliveryMovementId)).ToList();

        var existingDeliveryMovementIds = existingDeliveryMovements.Select(y => y.DeliveryMovementId).ToList();
        var updatedDeliveryMovements = command.DeliveryMovements.Where(updatedMovement =>
            updatedMovement.DeliveryMovementId != null &&
            existingDeliveryMovementIds.Contains(updatedMovement.DeliveryMovementId.Value)).ToList();
        
        if (addedDeliveryMovements.Any())
        {
            foreach (var addedDeliveryMovement in addedDeliveryMovements)
            {
                var createMovementRequest = GetCreateDeliveryMovementRequest(addedDeliveryMovement, command.NumberOfLoads, quote.QuoteId);
                await _quotesRepository.CreateDeliveryMovement(createMovementRequest);
            }
        }
        
        if (removedDeliveryMovements.Any())
        {
            foreach (var removedDeliveryMovement in removedDeliveryMovements)
            {
                await _quotesRepository.DeleteDeliveryMovement(removedDeliveryMovement.DeliveryMovementId);
            }
        }
        
        foreach (var updatedDeliveryMovement in updatedDeliveryMovements)
        {
            await _quotesRepository.UpdateDeliveryMovement(GetUpdateDeliveryMovementRequest(updatedDeliveryMovement, command.NumberOfLoads));
        }
    }

    private static UpdateDeliveryMovementRequest GetUpdateDeliveryMovementRequest(UpdateDeliveryMovementDto deliveryMovement, int numberOfLoads)
    {
        var updateDeliveryRequest = new UpdateDeliveryMovementRequest()
        {
            DeliveryMovementId = deliveryMovement.DeliveryMovementId.Value,
            VehicleTypeId = deliveryMovement.VehicleTypeId,
            NumberOfLoads = numberOfLoads,
            StartLocationLatitude = deliveryMovement.StartLocation.AddressPoint.Latitude,
            StartLocationLongitude = deliveryMovement.StartLocation.AddressPoint.Longitude,
            StartLocationFullAddress = deliveryMovement.StartLocation.FullAddress,
            DefaultOnewayJourneyTime = deliveryMovement.Pricing.DefaultOnewayJourneyTime,
            OnewayJourneyTime = deliveryMovement.Pricing.OnewayJourneyTime,
            DefaultTotalDeliveryPrice = deliveryMovement.Pricing.DefaultTotalDeliveryPrice,
            TotalDeliveryPrice = deliveryMovement.Pricing.TotalDeliveryPrice,
            DefaultDeliveryPricePerMinute = deliveryMovement.Pricing.DefaultDeliveryPricePerMinute,
            DeliveryPricePerMinute = deliveryMovement.Pricing.DeliveryPricePerMinute,
        };
        return updateDeliveryRequest;
    }
    
    private static CreateDeliveryMovementRequest GetCreateDeliveryMovementRequest(UpdateDeliveryMovementDto deliveryMovement, int numberOfLoads, int quoteId)
    {
        var updateDeliveryRequest = new CreateDeliveryMovementRequest()
        {
            QuoteId = quoteId,
            VehicleTypeId = deliveryMovement.VehicleTypeId,
            NumberOfLoads = numberOfLoads,
            StartLocationLatitude = deliveryMovement.StartLocation.AddressPoint.Latitude,
            StartLocationLongitude = deliveryMovement.StartLocation.AddressPoint.Longitude,
            StartLocationFullAddress = deliveryMovement.StartLocation.FullAddress,
            DefaultOnewayJourneyTime = deliveryMovement.Pricing.DefaultOnewayJourneyTime,
            OnewayJourneyTime = deliveryMovement.Pricing.OnewayJourneyTime,
            DefaultTotalDeliveryPrice = deliveryMovement.Pricing.DefaultTotalDeliveryPrice,
            TotalDeliveryPrice = deliveryMovement.Pricing.TotalDeliveryPrice,
            DefaultDeliveryPricePerDeliveryTimeUnit = deliveryMovement.Pricing.DefaultDeliveryPricePerMinute,
            DeliveryPricePerDeliveryTimeUnit = deliveryMovement.Pricing.DeliveryPricePerMinute,
        };
        return updateDeliveryRequest;
    }

    private static UpdateRecordRequest GetUpdateRecordRequest(UpdateDeliveryOnlyQuoteCommand command,
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