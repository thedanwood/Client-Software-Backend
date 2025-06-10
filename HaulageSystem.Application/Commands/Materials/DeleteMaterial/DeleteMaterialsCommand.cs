using HaulageSystem.Application.Models.Quotes;
using MediatR;

namespace HaulageSystem.Application.Core.Commands.Materials;

public class DeleteMaterialsCommand : IRequest
{
    public List<int> MaterialIds { get; set; }
}