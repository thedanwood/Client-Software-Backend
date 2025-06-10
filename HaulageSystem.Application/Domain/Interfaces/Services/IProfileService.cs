using HaulageSystem.Application.Domain.Entities.Profile;
using HaulageSystem.Application.Dtos.Quotes;
using HaulageSystem.Domain.Enums;

namespace HaulageSystem.Application.Domain.Interfaces.Services;

public interface IProfileService
{
    bool GetDefaultHasTrafficEnabled();
    DeliveryUnitDto GetDefaultDeliveryUnit();
    int GetQuotesFromDaysAgo();
    Dictionary<EmailUseCase, string> GetEmails();
    Task<EmailQuoteConfigResponse> GetEmailQuoteSettings();
    List<GetApplyMaximumCapacitySettingsResponse> GetApplyMaximumCapacitySettings();
}