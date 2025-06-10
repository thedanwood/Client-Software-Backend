using HaulageSystem.Application.Domain.Dtos.Companies;
using HaulageSystem.Application.Domain.Dtos.Materials;
using MediatR;

namespace HaulageSystem.Application.Core.Commands.Materials;

public class SearchCompaniesQuery : IRequest<List<CompanyDto>>
{
    public string? SearchTerm { get; set; }
}