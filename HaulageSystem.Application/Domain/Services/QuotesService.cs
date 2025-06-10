using HaulageSystem.Application.Core.Domain.Calculator;
using HaulageSystem.Application.Domain.Entities.Quotes;
using HaulageSystem.Application.Domain.Interfaces.Repositories;
using HaulageSystem.Application.Domain.Interfaces.Services;
using HaulageSystem.Application.Dtos.Quotes;
using HaulageSystem.Application.Exceptions;
using HaulageSystem.Application.Extensions;
using HaulageSystem.Application.Helpers;
using HaulageSystem.Application.Models.Quotes;
using HaulageSystem.Application.Models.Requests;
using HaulageSystem.Application.Models.Routing;
using HaulageSystem.Domain.Enums;

namespace HaulageSystem.Application.Domain.Services;

public class QuotesService : IQuotesService
{
    private readonly IBillingService _billingService;
    private readonly IDeliveryService _deliveryService;
    private readonly IMaterialsService _materialsService;
    private readonly IQuotesRepository _quotesRepository;
    private readonly IRoutingService _routingService;

    public QuotesService(
        IRoutingService routingService,
        IMaterialsService materialsService,
        IQuotesRepository quotesRepository,
        IBillingService billingService,
        IDeliveryService deliveryService)
    {
        _routingService = routingService;
        _quotesRepository = quotesRepository;
        _billingService = billingService;
        _deliveryService = deliveryService;
        _materialsService = materialsService;
    }
    
    public async Task<string> GetQuoteDeliveryInformationText(GetQuoteResponse quote)
    {
        switch ((QuoteType) quote.RecordType)
        {
            case QuoteType.HaulageOnly:
                return
                    $"Delivery on {quote.DeliveryDate.ToDateString()} to {quote.DeliveryLocationFullAddress}";
            case QuoteType.SupplyAndDelivery:
                return $"";
            default:
                throw new QuoteTypeNotFoundException(quote.QuoteId, quote.RecordType.ToString());
        }
    }
    public async Task<List<SavedSupplyDeliveryQuotePricingDto>> GetSavedSupplyDeliveryQuotePricing(GetQuoteResponse quote, List<GetDeliveryMovementResponse> deliveryMovements)
    {
        var responses = new List<SavedSupplyDeliveryQuotePricingDto>();

        foreach (var deliveryMovement in deliveryMovements)
        {
            var materialMovement = await _quotesRepository.GetMaterialMovementAsync(deliveryMovement.DeliveryMovementId);
            var totalMaterialAndDeliveryPrice =
                QuoteCalculator.CalculateTotalMaterialAndDeliveryPrice(deliveryMovement.TotalDeliveryPrice,
                    materialMovement.TotalMaterialPrice);
            var defaultTotalMaterialAndDeliveryPrice =
                QuoteCalculator.CalculateTotalMaterialAndDeliveryPrice(deliveryMovement.DefaultTotalDeliveryPrice,
                    materialMovement.DefaultTotalMaterialPrice);
            var totalQuantity = deliveryMovement.NumberOfLoads * materialMovement.Quantity;
            var materialAndDeliveryPricePerQuantityUnit =
                QuoteCalculator.CalculateMaterialAndDeliveryPricePerQuantityUnit(totalMaterialAndDeliveryPrice,
                    totalQuantity);
            var defaultMaterialAndDeliveryPricePerQuantityUnit =
                QuoteCalculator.CalculateMaterialAndDeliveryPricePerQuantityUnit(defaultTotalMaterialAndDeliveryPrice,
                    totalQuantity);
            
            responses.Add(new ()
            {
                DeliveryMovementId = deliveryMovement.DeliveryMovementId,
                DefaultOnewayJourneyTime = deliveryMovement.DefaultOnewayJourneyTime,
                OnewayJourneyTime = deliveryMovement.OnewayJourneyTime,
                MaterialAndDeliveryPricePerQuantityUnit = materialAndDeliveryPricePerQuantityUnit,
                DefaultMaterialAndDeliveryPricePerQuantityUnit = defaultMaterialAndDeliveryPricePerQuantityUnit,
                MaterialPricing = new ()
                {
                    MaterialMovementId = materialMovement.MaterialMovementId,
                    TotalMaterialPrice = materialMovement.TotalMaterialPrice,
                    DefaultTotalMaterialPrice = materialMovement.TotalMaterialPrice,
                    MaterialPricePerQuantityUnit = materialMovement.MaterialPricePerQuantityUnit,
                    DefaultMaterialPricePerQuantityUnit = materialMovement.DefaultMaterialPricePerQuantityUnit,
                    NumberOfLoads = deliveryMovement.NumberOfLoads
                },
                DeliveryPricing = new ()
                {
                    DefaultTotalDeliveryPrice = deliveryMovement.DefaultTotalDeliveryPrice,
                    TotalDeliveryPrice = deliveryMovement.TotalDeliveryPrice,
                    DeliveryPricePerTimeUnit = deliveryMovement.DeliveryPricePerTimeUnit,
                    DefaultDeliveryPricePerTimeUnit = deliveryMovement.DefaultDeliveryPricePerTimeUnit
                }
            });
        }

        return responses;
    }
}
