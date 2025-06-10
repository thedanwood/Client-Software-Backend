using HaulageSystem.Application.Models.Quotes;
using MediatR;

namespace HaulageSystem.Application.Core.Commands.Materials;

public class UpdateMaterialCommand : IRequest
{
    public int MaterialId { get; set; }
    public string MaterialName { get; set; }
}