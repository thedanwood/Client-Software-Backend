using HaulageSystem.Application.Domain.Dtos.Materials;
using HaulageSystem.Application.Dtos.Quotes;
using HaulageSystem.Application.Dtos.Settings;
using MediatR;

namespace HaulageSystem.Application.Core.Commands.Settings;

public class GetSettingsInitialDataQuery : IRequest<SettingsInitialDataDto>
{
}