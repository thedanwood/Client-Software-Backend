using System.Data;
using HaulageSystem.Application.Domain.Entities.Companies;
using HaulageSystem.Application.Domain.Entities.Database;
using HaulageSystem.Application.Domain.Interfaces.Repositories;
using HaulageSystem.Application.Domain.Interfaces.Services;
using HaulageSystem.Application.Domain.Mappers;
using HaulageSystem.Domain.Interfaces;
using HaulageSystem.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;

namespace HaulageSystem.Shared.Repositories;

public class CompaniesRepository : ICompaniesRepository
{
    private readonly HaulagePlannerDbContext _context;
    private readonly IUserService _userService;
    public CompaniesRepository(HaulagePlannerDbContext context, IUserService userService)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _userService = userService;
    }
    public async Task<List<GetCompanyResponse>> GetCompaniesAsync(List<int> companyIds)
    {
        var companies = await _context.Companies.Where(x => companyIds.Contains(x.CompanyId)).ToListAsync();
        return companies.Select(x => x.ToResponse()).ToList();
    }
    public async Task<List<GetCompanyResponse>> GetCompaniesAsync(string companyName, CancellationToken ct = default)
    {
        var companies = await _context.Companies.Where(x=> EF.Functions.Like(x.CompanyName, $"%{companyName}%")).ToListAsync(ct);
        return companies.Select(x => x.ToResponse()).ToList();
    }
    public async Task<GetCompanyResponse> GetCompanyByIdAsync(int companyId, CancellationToken ct = default)
    {
        var company = await _context.Companies.FirstOrDefaultAsync(x=>x.CompanyId == companyId, ct);
        if (company == null)
        {
            throw new DataException($"Company (id {companyId}) not found");
        }
        
        return new GetCompanyResponse()
        {
            CompanyName = company.CompanyName,
            CompanyId = company.CompanyId,
            PhoneNumber = company.Telephone,
            Email = company.Email
        };
    }
    public async Task<List<GetCompanyResponse>> GetCompaniesAsync(CancellationToken ct = default)
    {
        var companies = await  _context.Companies.ToListAsync(ct);
        return companies.Select(x => new GetCompanyResponse()
        {
            CompanyName = x.CompanyName,
            CompanyId = x.CompanyId,
            PhoneNumber = x.Telephone,
            Email = x.Email
        }).ToList();
    }
    public async Task UpdateCompanyAsync(int companyId, string email, string phone, string name, string updatedByUsername, CancellationToken ct = default)
    {
        var company = await  _context.Companies.FirstOrDefaultAsync(x => x.CompanyId == companyId, ct);
        company.Email = email;
        company.CompanyName = name;
        company.Telephone = phone;
        await _context.SaveChangesAsync(await _userService.GetCurrentUsername());
    }

    public async Task DeleteCompaniesAsync(List<int> companyIds, CancellationToken ct = default)
    {
        var company = await  _context.Companies.Where(x => companyIds.Contains(x.CompanyId)).ToListAsync(ct);
        _context.RemoveRange(company);
        await _context.SaveChangesAsync(await _userService.GetCurrentUsername());
    }

    public async Task<int> CreateCompanyAsync(string email, string phone, string name, CancellationToken ct = default)
    {
        var company = new Companies()
        {
            CompanyName = name,
            Email = email,
           Telephone = phone,
        };
        await _context.AddAsync(company);
        await _context.SaveChangesAsync(await _userService.GetCurrentUsername());
        return company.CompanyId;
    }
}