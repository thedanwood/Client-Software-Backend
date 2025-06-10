using HaulageSystem.Application.Commands.MaterialPricing;
using HaulageSystem.Application.Commands.Materials;
using HaulageSystem.Application.Core.Commands.Materials;
using HaulageSystem.Application.Domain.Dtos.Materials;
using HaulageSystem.Application.Dtos.Quotes;
using HaulageSystem.Application.Models.Routing;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HaulageSystem.Api.Controllers;

[Authorize]
[ApiVersion("1.0")]
public class MaterialPricingController : BaseApiController
{
    private readonly IMediator _mediator;
    public MaterialPricingController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
    [ProducesResponseType(typeof(MaterialPricingInitialDataDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpGet("initial-data")]
    public async Task<ActionResult<MaterialPricingInitialDataDto>> GetMaterialPricingInitialData([FromQuery]int depotId)
    {
        var x = await _mediator.Send(new GetMaterialPricingInitialDataCommand() { DepotId = depotId});
        return Ok(x);
    }

    [ProducesResponseType(typeof(DepotMaterialPricingInitialDataDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpGet("{depotId}")]
    public async Task<ActionResult<DepotMaterialPricingInitialDataDto>> GetDepotMaterialPricingsInitialData([FromRoute] int depotId)
    {
        var command = new GetDepotMaterialPricingsInitialDataQuery();
        command.DepotId = depotId;
        return Ok(await _mediator.Send(command));
    }

    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPost("")]
    public async Task<ActionResult> CreateMaterialPricing([FromBody] CreateMaterialPricingsCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }

    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPut("")]
    public async Task<ActionResult> UpdateMaterialPricing([FromBody] UpdateMaterialPricingCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }

    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpDelete("")]
    public async Task<ActionResult> DeleteMaterialPricings([FromBody] DeleteMaterialPricingCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }
}