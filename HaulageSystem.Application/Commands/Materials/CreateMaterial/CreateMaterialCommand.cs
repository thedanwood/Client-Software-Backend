using HaulageSystem.Application.Models.Quotes;
using MediatR;

namespace HaulageSystem.Application.Core.Commands.Materials;

public class CreateMaterialCommand : IRequest<int>
{
    public string MaterialName { get; set; }
}