using HaulageSystem.Application.Domain.Entities.Companies;

namespace HaulageSystem.Application.Domain.Interfaces.Repositories;

public interface ICompaniesRepository
{
    Task<List<GetCompanyResponse>> GetCompaniesAsync(string searchTerm, CancellationToken ct = default);
    Task<List<GetCompanyResponse>> GetCompaniesAsync(CancellationToken ct = default);
    Task<List<GetCompanyResponse>> GetCompaniesAsync(List<int> companyIds);
    Task<GetCompanyResponse> GetCompanyByIdAsync(int companyId, CancellationToken ct = default);
    Task UpdateCompanyAsync(int companyId, string email, string phone, string name, string updatedByUsername, CancellationToken ct = default);
    Task DeleteCompaniesAsync(List<int> companyIds, CancellationToken ct = default);
    Task<int> CreateCompanyAsync(string email, string phone, string name, CancellationToken ct = default);
}