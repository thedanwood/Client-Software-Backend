using HaulageSystem.Application.Core.Commands.Companies;
using HaulageSystem.Application.Domain.Interfaces.Repositories;
using HaulageSystem.Application.Domain.Interfaces.Services;
using MediatR;
using OneOf.Types;

namespace HaulageSystem.Application.Commands.Materials;

public class DeleteCompaniesCommandHandler : IRequestHandler<DeleteCompaniesCommand>
{
    private readonly ICompaniesRepository _companiesRepository;
    private readonly IUserService _userService;

    public DeleteCompaniesCommandHandler(ICompaniesRepository companiesRepository, IUserService userService)
    {
        _companiesRepository = companiesRepository;
        _userService = userService;
    }

    public async Task Handle(DeleteCompaniesCommand command, CancellationToken cancellationToken)
    {
        await _companiesRepository.DeleteCompaniesAsync(command.CompanyIds, cancellationToken);
    }
}