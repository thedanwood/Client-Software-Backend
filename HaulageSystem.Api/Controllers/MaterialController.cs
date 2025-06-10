using HaulageSystem.Application.Commands.Materials;
using HaulageSystem.Application.Core.Commands.Companies;
using HaulageSystem.Application.Core.Commands.Materials;
using HaulageSystem.Application.Domain.Dtos.Materials;
using HaulageSystem.Application.Dtos.Materials;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HaulageSystem.Api.Controllers;

[Authorize]
[ApiVersion("1.0")]
public class MaterialController : BaseApiController
{
    private readonly IMediator _mediator;
    public MaterialController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
    [ProducesResponseType(typeof(List<MaterialInformationDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpGet("")]
    public async Task<ActionResult<List<MaterialInformationDto>>> GetMaterials()
    {
        return Ok(await _mediator.Send(new GetMaterialsQuery()));
    }

    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPut("{materialId}")]
    public async Task<ActionResult> UpdateMaterial([FromRoute] int materialId, [FromBody]UpdateMaterialCommand command)
    {
        command.MaterialId = materialId;
        await _mediator.Send(command);
        return Ok();
    }

    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpDelete("")]
    public async Task<ActionResult> DeleteMaterial([FromBody] DeleteMaterialsCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }

    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPost("")]
    public async Task<ActionResult<int>> CreateMaterial([FromBody] CreateMaterialCommand command)
    {
        return Ok(await _mediator.Send(command));        
    }
}