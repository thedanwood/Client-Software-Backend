using HaulageSystem.Application.Commands.Materials;
using HaulageSystem.Application.Core.Commands.Companies;
using HaulageSystem.Application.Core.Commands.Materials;
using HaulageSystem.Application.Domain.Dtos.Companies;
using HaulageSystem.Application.Domain.Dtos.Materials;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HaulageSystem.Api.Controllers;

[Authorize]
[ApiVersion("1.0")]
public class CompanyController : BaseApiController
{
    private readonly IMediator _mediator;
    public CompanyController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
    [ProducesResponseType(typeof(List<CompanyDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpGet("search")]
    public async Task<ActionResult<List<CompanyDto>>> SearchCompanies([FromQuery] SearchCompaniesQuery query)
    {
        return Ok(await _mediator.Send(query));
    }

    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPut("{companyId}")]
    public async Task<ActionResult> UpdateCompany([FromRoute] int companyId, [FromBody]UpdateCompanyCommand command)
    {
        command.CompanyId = companyId;
        await _mediator.Send(command);
        return Ok();
    }

    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpDelete("")]
    public async Task<ActionResult> DeleteCompanies([FromBody] DeleteCompaniesCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }

    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPost("")]
    public async Task<ActionResult<int>> CreateCompany([FromBody] CreateCompanyCommand command)
    {
        return Ok(await _mediator.Send(command));        
    }
}