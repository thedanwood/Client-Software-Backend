using HaulageSystem.Application.Core.Commands.Companies;
using HaulageSystem.Application.Domain.Interfaces.Repositories;
using HaulageSystem.Application.Domain.Interfaces.Services;
using MediatR;
using OneOf.Types;

namespace HaulageSystem.Application.Commands.Materials;

public class UpdateCompaniesCommandHandler : IRequestHandler<UpdateCompanyCommand>
{
    private readonly ICompaniesRepository _companiesRepository;
    private readonly IUserService _userService;

    public UpdateCompaniesCommandHandler(ICompaniesRepository companiesRepository, IUserService userService)
    {
        _companiesRepository = companiesRepository;
        _userService = userService;
    }

    public async Task Handle(UpdateCompanyCommand command, CancellationToken cancellationToken)
    {
        var username = await _userService.GetCurrentUsername();
        await _companiesRepository.UpdateCompanyAsync(command.CompanyId, command.Email, command.Phone, command.Name, username, cancellationToken);
    }
}