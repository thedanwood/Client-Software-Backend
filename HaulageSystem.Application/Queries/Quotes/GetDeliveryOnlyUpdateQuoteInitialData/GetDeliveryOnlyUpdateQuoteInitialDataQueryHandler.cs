using HaulageSystem.Application.Core.Domain.Calculator;
using HaulageSystem.Application.Domain.Dtos.Materials;
using HaulageSystem.Application.Domain.Dtos.Vehicles;
using HaulageSystem.Application.Domain.Interfaces.Repositories;
using HaulageSystem.Application.Domain.Interfaces.Services;
using HaulageSystem.Application.Dtos.Quotes;
using HaulageSystem.Application.Helpers;
using HaulageSystem.Application.Models.Quotes;
using HaulageSystem.Application.Models.Routing;
using HaulageSystem.Domain.Enums;
using MediatR;

namespace HaulageSystem.Application.Queries.Quotes;

public class GetDeliveryOnlyUpdateQuoteInitialDataHandler : IRequestHandler<GetDeliveryOnlyUpdateQuoteInitialDataQuery, UpdateQuoteDeliveryOnlyInitialDataDto>
{
    private readonly IDeliveryService _deliveryService;
    private readonly IQuotesRepository _quotesRepository;
    private readonly IQuotesService _quotesService;
    private readonly IVehiclesRepository _vehiclesRepository;
    private readonly IRoutingService _routingService;
    private readonly IProfileService _profileService;
    private readonly IMediator _mediator;
    private readonly ICompaniesRepository _companiesRepository;
    public GetDeliveryOnlyUpdateQuoteInitialDataHandler(IQuotesRepository quotesRepository, IVehiclesRepository vehiclesRepository, IDeliveryService deliveryService, ICompaniesRepository companiesRepository, IQuotesService quotesService, IMediator mediator, IProfileService profileService, IRoutingService routingService)
    {
        _quotesRepository = quotesRepository;
        _vehiclesRepository = vehiclesRepository;
        _deliveryService = deliveryService;
        _companiesRepository = companiesRepository;
        _quotesService = quotesService;
        _mediator = mediator;
        _profileService = profileService;
        _routingService = routingService;
    }
    public async Task<UpdateQuoteDeliveryOnlyInitialDataDto> Handle(GetDeliveryOnlyUpdateQuoteInitialDataQuery query, CancellationToken cancellationToken)
    {
        var quote = await _quotesRepository.GetQuote(query.QuoteId);
        var deliveryMovements = await _quotesRepository.GetDeliveryMovementsAsync(query.QuoteId);
        var initialData = await _mediator.Send(new GetDeliveryOnlyQuoteInitialDataQuery());
        var companyInfo = await _companiesRepository.GetCompanyByIdAsync(quote.CompanyID);
        
        var updateDto = new UpdateQuoteDeliveryOnlyInitialDataDto()
        {
            VehicleTypes = initialData.VehicleTypes,
            DeliveryDate = quote.DeliveryDate,
            Comments = quote.Comments,
            CompanyInfo = new()
            {
                Name = companyInfo.CompanyName,
                Id = companyInfo.CompanyId
            },
            CustomerName = quote.CustomerName,
            NumberOfLoads = deliveryMovements[0].NumberOfLoads,
            DeliveryLocation = new AddressDto(new(quote.DeliveryLocationLatitude, quote.DeliveryLocationLongitude), quote.DeliveryLocationFullAddress),
            DefaultHasTrafficEnabled = initialData.DefaultHasTrafficEnabled
        };
        var updateDeliveryMovements = new List<UpdateQuoteDeliveryOnlyDeliveryMovementDto>();
        foreach (var deliveryMovement in deliveryMovements)
        {
            var startLocation = new RoutePoint(deliveryMovement.StartLocationLatitude.Value, deliveryMovement.StartLocationLongitude.Value);
            var endLocation = new RoutePoint(quote.DeliveryLocationLatitude, quote.DeliveryLocationLongitude);
            var journeyTimes = new List<JouneyTimeHasTrafficDto>();
            
            journeyTimes.Add(new()
            {
                JourneyTime = deliveryMovement.OnewayJourneyTime,
                HasTrafficEnabled = deliveryMovement.HasTrafficEnabled
            });
            journeyTimes.Add(await _routingService.GetJourneyTimeWithHasTrafficEnabled(startLocation, endLocation, !deliveryMovement.HasTrafficEnabled));

            updateDeliveryMovements.Add(new UpdateQuoteDeliveryOnlyDeliveryMovementDto()
            {
                StartLocation = new AddressDto(startLocation, deliveryMovement.StartLocationFullAddress),
                DeliveryMovementId = deliveryMovement.DeliveryMovementId,
                DefaultOnewayJourneyTime = deliveryMovement.DefaultOnewayJourneyTime,
                DefaultTotalDeliveryPrice = deliveryMovement.DefaultTotalDeliveryPrice,
                TotalDeliveryPrice = deliveryMovement.TotalDeliveryPrice,
                DefaultDeliveryPricePerMinute = deliveryMovement.DefaultDeliveryPricePerTimeUnit,
                DeliveryPricePerMinute = deliveryMovement.DeliveryPricePerTimeUnit,
                HasTrafficEnabled = deliveryMovement.HasTrafficEnabled,
                JourneyTimes = journeyTimes,
                VehicleTypeId = deliveryMovement.VehicleTypeId
            });
        }

        updateDto.DeliveryMovements = updateDeliveryMovements;
        return updateDto;
    }
}