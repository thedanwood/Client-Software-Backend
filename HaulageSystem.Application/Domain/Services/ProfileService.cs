using HaulageSystem.Application.Domain.Dtos.Vehicles;
using HaulageSystem.Application.Domain.Entities.Profile;
using HaulageSystem.Application.Domain.Interfaces.Repositories;
using HaulageSystem.Application.Domain.Interfaces.Services;
using HaulageSystem.Application.Dtos.Quotes;
using HaulageSystem.Application.Helpers;
using HaulageSystem.Application.Models.Requests;
using HaulageSystem.Domain.Enums;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;

namespace HaulageSystem.Application.Domain.Services;

public class ProfileService: IProfileService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<ProfileService> _logger;
    private readonly IServiceScope _scope;
    
    public ProfileService(ILogger<ProfileService> logger, IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
        _scope = _serviceScopeFactory.CreateScope();
    }
    public int GetQuotesFromDaysAgo()
    {
        return 60;
    }
    public bool GetDefaultHasTrafficEnabled()
    {
        return false;
    }
    public DeliveryUnitDto GetDefaultDeliveryUnit()
    {
        var unit = DeliveryTimeUnits.Minutes;
        return new DeliveryUnitDto()
        {
            DeliveryUnitId = unit.ToInt(),
            DeliveryUnitName = unit.ToDescription()
        };
    }

    public Dictionary<EmailUseCase, string> GetEmails()
    {
        return new() {
            { EmailUseCase.SendQuote, "no-reply@roryjholbrook.co.uk" }
        };
    }
    
    public async Task<EmailQuoteConfigResponse> GetEmailQuoteSettings()
    {
        var userService = _scope.ServiceProvider.GetService<IUserService>();
        var ccUsernames = new List<string>() {"benw", "jakew", "connorg", "jamesp", "maggiew"};
        var ccEmails = new List<(string email, string fullName)>();

        
        foreach (var ccEmailUsername in ccUsernames)
        {
            try
            {
                var user = await userService.GetUserInfo(ccEmailUsername);
                ccEmails.Add((email: user.Email, fullName: user.FullName));
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Cc email {ccEmailUsername} configured in profile service not found");
            }
        }
            
        return new() {
            CcEmails = ccEmails.Select(x => new SendEmailAddressRequest(x.email, x.fullName)).ToList()
        };
    }
    
    public List<GetApplyMaximumCapacitySettingsResponse> GetApplyMaximumCapacitySettings()
    {
        return new()
        {
            new()
            {
                MaterialUnitId = MaterialUnits.Tonnes.ToInt(),
                //8 wheeler
                //TODO: dont rely on id
                ApplyMaximumCapacityFromVehicleTypes = new() { 1 }
            }
        };
    }
}