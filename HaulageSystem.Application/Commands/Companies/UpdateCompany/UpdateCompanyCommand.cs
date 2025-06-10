using HaulageSystem.Application.Models.Quotes;
using MediatR;

namespace HaulageSystem.Application.Core.Commands.Companies;

public class UpdateCompanyCommand : IRequest
{
    public int CompanyId { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Name { get; set; }
}