using HaulageSystem.Application.Domain.Dtos.Materials;
using HaulageSystem.Application.Domain.Entities.Quotes;
using HaulageSystem.Application.Domain.Services;
using HaulageSystem.Application.Dtos.Quotes;
using HaulageSystem.Application.Models.Quotes;
using HaulageSystem.Domain.Enums;

namespace HaulageSystem.Application.Domain.Interfaces.Services;

public interface IQuotesService
{
    Task<string> GetQuoteDeliveryInformationText(GetQuoteResponse quote);

    Task<List<SavedSupplyDeliveryQuotePricingDto>> GetSavedSupplyDeliveryQuotePricing(GetQuoteResponse quote,
        List<GetDeliveryMovementResponse> deliveryMovements);
}