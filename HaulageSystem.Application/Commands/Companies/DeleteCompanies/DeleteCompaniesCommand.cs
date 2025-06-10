using HaulageSystem.Application.Models.Quotes;
using MediatR;

namespace HaulageSystem.Application.Core.Commands.Companies;

public class DeleteCompaniesCommand : IRequest
{
    public List<int> CompanyIds { get; set; }
}