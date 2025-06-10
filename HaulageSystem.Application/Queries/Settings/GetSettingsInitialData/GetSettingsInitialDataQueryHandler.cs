using System.Globalization;
using HaulageSystem.Application.Core.Commands.Materials;
using HaulageSystem.Application.Core.Commands.Quotes;
using HaulageSystem.Application.Core.Commands.Settings;
using HaulageSystem.Application.Core.Domain.Calculator;
using HaulageSystem.Application.Domain.Dtos.Materials;
using HaulageSystem.Application.Domain.Dtos.Quotes;
using HaulageSystem.Application.Domain.Interfaces.Repositories;
using HaulageSystem.Application.Domain.Interfaces.Services;
using HaulageSystem.Application.Dtos.Materials;
using HaulageSystem.Application.Dtos.Profile;
using HaulageSystem.Application.Dtos.Quotes;
using HaulageSystem.Application.Dtos.Settings;
using HaulageSystem.Application.Exceptions;
using HaulageSystem.Application.Extensions;
using HaulageSystem.Application.Helpers;
using HaulageSystem.Application.Models.Requests;
using HaulageSystem.Application.Models.Routing;
using HaulageSystem.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Serilog;

namespace HaulageSystem.Application.Commands.Settings;

public class GetSettingsInitialDataQueryHandler : IRequestHandler<GetSettingsInitialDataQuery, SettingsInitialDataDto>
{
    private readonly IProfileService _profileService;
    public GetSettingsInitialDataQueryHandler(IProfileService profileService)
    {
        _profileService = profileService;
    }
    public async Task<SettingsInitialDataDto> Handle(GetSettingsInitialDataQuery query,
        CancellationToken cancellationToken)
    {
        return new()
        {
            DeliveryUnit = _profileService.GetDefaultDeliveryUnit(),
            ApplyMaximumCapacitySettings = _profileService.GetApplyMaximumCapacitySettings().Select(x => new ApplyMaximumCapacitySettingDto()
            {
                MaterialUnitId = x.MaterialUnitId,
                ApplyMaximumCapacityFromVehicleTypes = x.ApplyMaximumCapacityFromVehicleTypes
            }).ToList()
        };
    }
}