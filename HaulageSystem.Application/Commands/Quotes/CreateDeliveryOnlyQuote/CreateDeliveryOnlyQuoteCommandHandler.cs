using HaulageSystem.Application.Core.Commands.Quotes;
using HaulageSystem.Application.Domain.Interfaces.Repositories;
using HaulageSystem.Application.Domain.Interfaces.Services;
using HaulageSystem.Domain.Enums;
using MediatR;

namespace HaulageSystem.Application.Commands.Quotes;

public class CreateDeliveryOnlyQuoteCommandHandler : IRequestHandler<CreateDeliveryOnlyQuoteCommand, int>
{
    private readonly IQuotesRepository _quotesRepository;
    private readonly IQuotesService _quotesService;
    private readonly IUserService _userService;

    public CreateDeliveryOnlyQuoteCommandHandler(IQuotesRepository quotesRepository,
        IUserService userService, IQuotesService quotesService)
    {
        _quotesRepository = quotesRepository;
        _userService = userService;
        _quotesService = quotesService;
    }

    public async Task<int> Handle(CreateDeliveryOnlyQuoteCommand command,
        CancellationToken cancellationToken)
    {
        var quoteId =
            await _quotesRepository.CreateRecordAsync(new()
            {
                RecordType = (int)RecordTypes.DeliveryOnly,
                RecordVariation = (int)RecordVariations.Quote,
                ActiveStateEnumValue = (int)QuoteActiveStates.Active,
                Comments = command.Comments,
                CompanyId = command.CompanyId,
                CreatedByUsername = await _userService.GetCurrentUsername(),
                CustomerName = command.CustomerName,
                DeliveryDate = command.DeliveryDateTime,
                DeliveryLocationFullAddress = command.DeliveryLocation.FullAddress,
                DeliveryLocationLatitude = command.DeliveryLocation.AddressPoint.Latitude,
                DeliveryLocationLongitude = command.DeliveryLocation.AddressPoint.Longitude,
            });
        
        foreach (var deliveryMovement in command.DeliveryMovements)
        {
            await _quotesRepository.CreateDeliveryMovement(new()
            {
                QuoteId = quoteId,
                StartLocationLatitude = deliveryMovement.StartLocation.AddressPoint.Latitude,
                StartLocationLongitude = deliveryMovement.StartLocation.AddressPoint.Longitude,
                StartLocationFullAddress = deliveryMovement.StartLocation.FullAddress,
                NumberOfLoads = command.NumberOfLoads,
                VehicleTypeId = deliveryMovement.VehicleTypeId,
                DefaultOnewayJourneyTime = deliveryMovement.Pricing.DefaultOnewayJourneyTime,
                OnewayJourneyTime = deliveryMovement.Pricing.OnewayJourneyTime,
                TotalDeliveryPrice = deliveryMovement.Pricing.TotalDeliveryPrice,
                DefaultTotalDeliveryPrice = deliveryMovement.Pricing.DefaultTotalDeliveryPrice,
                DeliveryPricePerDeliveryTimeUnit = deliveryMovement.Pricing.DeliveryPricePerMinute,
                DefaultDeliveryPricePerDeliveryTimeUnit = deliveryMovement.Pricing.DefaultDeliveryPricePerMinute
            });

        }

        return quoteId;
    }
}