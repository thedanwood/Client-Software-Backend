using HaulageSystem.Application.Models.Quotes;
using MediatR;

namespace HaulageSystem.Application.Core.Commands.Materials;

public class DeleteDepotsCommand : IRequest
{
    public List<int> DepotIds { get; set; }
}