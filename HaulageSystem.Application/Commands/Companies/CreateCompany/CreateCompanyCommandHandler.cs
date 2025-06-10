using HaulageSystem.Application.Core.Commands.Companies;
using HaulageSystem.Application.Domain.Interfaces.Repositories;
using MediatR;
using OneOf.Types;

namespace HaulageSystem.Application.Commands.Materials;

public class AddCompaniesCommandHandler : IRequestHandler<CreateCompanyCommand, int>
{
    private readonly ICompaniesRepository _companiesRepository;

    public AddCompaniesCommandHandler(ICompaniesRepository companiesRepository)
    {
        _companiesRepository = companiesRepository;
    }

    public async Task<int> Handle(CreateCompanyCommand command, CancellationToken cancellationToken)
    {
        return await _companiesRepository.CreateCompanyAsync(command.Email, command.Phone, command.Name, cancellationToken);
    }
}