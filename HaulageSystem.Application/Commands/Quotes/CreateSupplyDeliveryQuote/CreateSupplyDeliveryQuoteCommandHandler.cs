using AutoMapper;
using HaulageSystem.Application.Core.Commands.Quotes;
using HaulageSystem.Application.Domain.Entities.Database;
using HaulageSystem.Application.Domain.Interfaces.Repositories;
using HaulageSystem.Application.Domain.Interfaces.Services;
using HaulageSystem.Application.Dtos.Materials;
using HaulageSystem.Application.Helpers;
using HaulageSystem.Application.Mappers;
using HaulageSystem.Application.Models.Quotes;
using HaulageSystem.Application.Models.Requests;
using HaulageSystem.Application.Models.Routing;
using HaulageSystem.Domain.Enums;
using MediatR;

namespace HaulageSystem.Application.Commands.Quotes;

public class CreateSupplyDeliveryQuoteCommandHandler : IRequestHandler<CreateSupplyDeliveryQuoteCommand, int>
{
    private readonly IRoutingService _routingService;
    private readonly IDepotsRepository _depotsRepository;
    private readonly IBillingService _billingService;
    private readonly IUserService _userService;
    private readonly IMaterialPricingRepository _materialPricingRepository;
    private readonly IQuotesRepository _quotesRepository;
    private readonly IDeliveryService _deliveryService;

    public CreateSupplyDeliveryQuoteCommandHandler(
        IRoutingService routingService,
        IBillingService billingService, 
        IMaterialPricingRepository materialPricingRepository,
        IDepotsRepository depotsRepository, 
        IUserService userService, 
        IQuotesRepository quotesRepository,
        IDeliveryService deliveryService)
    {
        _routingService = routingService;
        _billingService = billingService;
        _materialPricingRepository = materialPricingRepository;
        _depotsRepository = depotsRepository;
        _userService = userService;
        _quotesRepository = quotesRepository;
        _deliveryService = deliveryService;
    }

    public async Task<int> Handle(CreateSupplyDeliveryQuoteCommand command, CancellationToken cancellationToken)
    {
        var QuoteId = await CreateRecord(command);

        foreach (var materialMovementCommand in command.MaterialMovements)
        {
            var deliveryMovementId = await CreateDeliveryMovement(QuoteId, materialMovementCommand, command);

            await CreateMaterialMovement(deliveryMovementId, materialMovementCommand);
        }

        return QuoteId;
    }

    private async Task<int> CreateRecord(CreateSupplyDeliveryQuoteCommand command)
    {
        var recordRequest = new CreateRecordRequest()
        {
            RecordType = RecordTypes.SupplyAndDelivery.ToInt(),
            RecordVariation = RecordVariations.Quote.ToInt(),
            DeliveryDate = command.DeliveryDateTime,
            CustomerName = command.CustomerName,
            CompanyId = command.CompanyId,
            Comments = command.Comments,
            DeliveryLocationFullAddress = command.DeliveryLocation.FullAddress,
            DeliveryLocationLatitude = command.DeliveryLocation.AddressPoint.Latitude,
            DeliveryLocationLongitude = command.DeliveryLocation.AddressPoint.Longitude,
            CreatedByUsername = await _userService.GetCurrentUsername(),
            ActiveStateEnumValue = QuoteActiveStates.Active.ToInt()
        };
        return await _quotesRepository.CreateRecordAsync(recordRequest);
    }

    private async Task<int> CreateDeliveryMovement(int quoteId, CreateMaterialMovementCommand materialMovementCommand,
        CreateSupplyDeliveryQuoteCommand command)
    {
        var depotId =
            (await _materialPricingRepository.GetMaterialPricingByDepotMaterialPriceId(materialMovementCommand
                .DepotMaterialPriceId)).DepotId;
        var depot = await _depotsRepository.GetDepot(depotId);
        var depotLocation = new RoutePoint(depot.Latitude,depot.Longitude);

        var deliveryMovementRequest = new CreateDeliveryMovementRequest()
        {
            StartLocationLatitude = depot.Latitude,
            StartLocationLongitude = depot.Longitude,
            StartLocationFullAddress = depot.DepotName,
            QuoteId = quoteId,
            OnewayJourneyTime = materialMovementCommand.OnewayJourneyTime,
            DefaultOnewayJourneyTime = materialMovementCommand.OnewayJourneyTime,
            VehicleTypeId = materialMovementCommand.VehicleTypeId,
            NumberOfLoads = await _deliveryService.GetNumberOfLoads((MaterialUnits)materialMovementCommand.MaterialUnitId,
                materialMovementCommand.Quantity, materialMovementCommand.VehicleTypeId),
            DeliveryPricePerDeliveryTimeUnit = materialMovementCommand.Pricing.DeliveryPricePerTimeUnit,
            TotalDeliveryPrice = materialMovementCommand.Pricing.TotalDeliveryPrice,
            DefaultDeliveryPricePerDeliveryTimeUnit = materialMovementCommand.Pricing.DefaultDeliveryPricePerTimeUnit,
            DefaultTotalDeliveryPrice = materialMovementCommand.Pricing.DefaultTotalDeliveryPrice,
            HasTrafficEnabled = materialMovementCommand.HasTrafficEnabled
        };
        return await _quotesRepository.CreateDeliveryMovement(deliveryMovementRequest);
    }

    private async Task CreateMaterialMovement(int deliveryMovementId, CreateMaterialMovementCommand materialMovementCommand)
    {
        var materialMovementRequest = new CreateMaterialMovementRequest()
        {
            DeliveryMovementId = deliveryMovementId,
            Quantity = materialMovementCommand.Quantity,
            DepotMaterialPriceId = materialMovementCommand.DepotMaterialPriceId,
            MaterialUnitId = materialMovementCommand.MaterialUnitId,
            DefaultTotalMaterialPrice = materialMovementCommand.Pricing.DefaultTotalMaterialPrice,
            DefaultMaterialPricePerQuantityUnit = materialMovementCommand.Pricing.DefaultMaterialPricePerQuantityUnit,
            TotalMaterialPrice = materialMovementCommand.Pricing.TotalMaterialPrice,
            MaterialPricePerQuantityUnit = materialMovementCommand.Pricing.MaterialPricePerQuantityUnit
        };
        await  _quotesRepository.CreateMaterialMovement(materialMovementRequest);
    }
}