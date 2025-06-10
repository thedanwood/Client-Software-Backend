using HaulageSystem.Application.Dtos.Depots;
using HaulageSystem.Application.Dtos.Materials;
using HaulageSystem.Application.Models.Quotes;
using MediatR;

namespace HaulageSystem.Application.Core.Commands.Materials;

public class GetDepotsQuery : IRequest<List<DepotsInformationDto>>
{
}