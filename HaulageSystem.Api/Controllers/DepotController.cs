using HaulageSystem.Application.Commands.Depots.CreateDepot;
using HaulageSystem.Application.Commands.Materials;
using HaulageSystem.Application.Core.Commands.Companies;
using HaulageSystem.Application.Core.Commands.Depots;
using HaulageSystem.Application.Core.Commands.Materials;
using HaulageSystem.Application.Domain.Dtos.Materials;
using HaulageSystem.Application.Dtos.Depots;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HaulageSystem.Api.Controllers;

[Authorize]
[ApiVersion("1.0")]
public class DepotController : BaseApiController
{
    private readonly IMediator _mediator;
    public DepotController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
    [ProducesResponseType(typeof(List<DepotsInformationDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpGet("")]
    public async Task<ActionResult<List<DepotsInformationDto>>> GetDepots()
    {
        return Ok(await _mediator.Send(new GetDepotsQuery()));
    }

    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPut("{depotId}")]
    public async Task<ActionResult> UpdateDepot([FromRoute] int depotId, [FromBody]UpdateDepotCommand command)
    {
        command.DepotId = depotId;
        await _mediator.Send(command);
        return Ok();
    }

    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpDelete("")]
    public async Task<ActionResult> DeleteDepots([FromBody] DeleteDepotsCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }

    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPost("")]
    public async Task<ActionResult<int>> CreateDepot([FromBody] CreateDepotCommand command)
    {
        return Ok(await _mediator.Send(command));        
    }
}