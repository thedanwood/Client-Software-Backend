using HaulageSystem.Application.Domain.Dtos.Materials;
using HaulageSystem.Application.Models.Routing;
using MediatR;

namespace HaulageSystem.Application.Core.Commands.Materials;

public class GetDepotMaterialPricingsInitialDataQuery : IRequest<DepotMaterialPricingInitialDataDto>
{
    public int DepotId { get; set; }
}