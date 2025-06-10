using HaulageSystem.Application.Core.Commands.Materials;
using HaulageSystem.Application.Domain.Dtos.Companies;
using HaulageSystem.Application.Domain.Dtos.Materials;
using HaulageSystem.Application.Domain.Entities.Companies;
using HaulageSystem.Application.Domain.Interfaces.Repositories;
using HaulageSystem.Application.Dtos.Depots;
using MediatR;

namespace HaulageSystem.Application.Commands.Materials;

public class SearchCompaniesQueryHandler : IRequestHandler<SearchCompaniesQuery, List<CompanyDto>>
{
    private readonly ICompaniesRepository _companiesRepository;

    public SearchCompaniesQueryHandler(ICompaniesRepository companiesRepository)
    {
        _companiesRepository = companiesRepository;
    }

    public async Task<List<CompanyDto>> Handle(SearchCompaniesQuery query, CancellationToken ct)
    {
        var companies = new List<GetCompanyResponse>();
        if(query.SearchTerm == null)
        {
            companies = await _companiesRepository.GetCompaniesAsync();
        }
        else
        {
            companies = await _companiesRepository.GetCompaniesAsync(query.SearchTerm, ct);
        }
        
        return companies.Select(x => new CompanyDto()
        {
            Id = x.CompanyId,
            Name = x.CompanyName,
            Email = x.Email,
            Phone = x.PhoneNumber
        }).ToList().OrderBy(x=>x.Name).ToList();
    }
}