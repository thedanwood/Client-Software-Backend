using HaulageSystem.Application.Core.Domain.Calculator;
using HaulageSystem.Application.Domain.Interfaces.Repositories;
using HaulageSystem.Application.Domain.Interfaces.Services;
using HaulageSystem.Application.Dtos.Quotes;
using HaulageSystem.Application.Exceptions;
using HaulageSystem.Application.Extensions;

namespace HaulageSystem.Application.Domain.Services;

public class BillingService : IBillingService
{
    private readonly IMaterialPricingRepository _materialPricingRepository;

    public BillingService(IMaterialPricingRepository materialPricingRepository)
    {
        _materialPricingRepository = materialPricingRepository ??  throw new ArgumentNullException(nameof(materialPricingRepository));
    }

    public async Task<HaulageQuotePriceResponseDto> CalculateDeliveryMovementQuotePriceAsync(HaulageQuotePriceRequestDto dto)
    {
        var deliveryPricePerDeliveryTimeUnit = QuoteCalculator.CalculateDeliveryPricePerDeliveryTimeUnit(dto.VehicleTypeId, dto.OnewayJourneyTime);
        var totalDeliveryPrice = QuoteCalculator.CalculateTotalDeliveryPrice(dto.NumberOfLoads, dto.OnewayJourneyTime, deliveryPricePerDeliveryTimeUnit);
        return new HaulageQuotePriceResponseDto()
        {
            TotalDeliveryPrice = totalDeliveryPrice,
            DeliveryPricePerTimeUnit = deliveryPricePerDeliveryTimeUnit
        };
    }

    public async Task<MaterialPriceResponseDto> CalculateMaterialMovementQuotePrice(int DepotMaterialPriceId, int totalQuantity)
    {
        var materialPricing = await _materialPricingRepository.GetMaterialPricingByDepotMaterialPriceId(DepotMaterialPriceId);
        var totalMaterialPrice = (materialPricing.MaterialPricePerQuantityUnit * totalQuantity).ToPrice();
        return new MaterialPriceResponseDto
        {
            TotalMaterialPrice = totalMaterialPrice,
            DefaultTotalMaterialPrice = totalMaterialPrice,
            MaterialPricePerQuantityUnit = materialPricing.MaterialPricePerQuantityUnit,
            DefaultMaterialPricePerQuantityUnit = materialPricing.MaterialPricePerQuantityUnit
        };
    }
}