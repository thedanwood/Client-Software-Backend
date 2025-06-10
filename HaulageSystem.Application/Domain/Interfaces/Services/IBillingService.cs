using HaulageSystem.Application.Dtos.Quotes;

namespace HaulageSystem.Application.Domain.Interfaces.Services;

public interface IBillingService
{
    Task<HaulageQuotePriceResponseDto> CalculateDeliveryMovementQuotePriceAsync(HaulageQuotePriceRequestDto dto);

    Task<MaterialPriceResponseDto> CalculateMaterialMovementQuotePrice(int DepotMaterialPriceId, int quantity);
}